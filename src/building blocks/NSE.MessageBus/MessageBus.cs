using EasyNetQ;
using NSE.Core.Messages.Integration;
using Polly;
using RabbitMQ.Client.Exceptions;
using System;
using System.Threading.Tasks;

namespace NSE.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private IBus _bus;
        private IAdvancedBus _advancedBus;

        private readonly string _connectionString;

        public MessageBus(string connectionString)
        {
            _connectionString = connectionString;
            TryConnect();
        }

        public bool IsConnected => _bus?.IsConnected ?? false;
        public IAdvancedBus AdvancedBus => _bus?.Advanced;
        
        public void Publish<T>(T message) where T : IntegrationEvent
        {
            TryConnect();
            _bus.Publish(message);
        }

        public async Task PublishAsync<T>(T message) where T : IntegrationEvent
        {
            TryConnect();
            await _bus.PublishAsync(message);
        }
        public void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class
        {
            TryConnect();
            _bus.Subscribe(subscriptionId, onMessage);
        }

        public void SubscribeAsync<T>(string subscriptionID, Func<T, Task> onMessage) where T : class
        {
            TryConnect();
            _bus.SubscribeAsync(subscriptionID, onMessage);
        }

        public TResponce Request<TRequest, TResponce>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponce : ResponseMessage
        {
            TryConnect();
            return _bus.Request<TRequest, TResponce>(request);
        }

        public async Task<TResponce> RequestAsync<TRequest, TResponce>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponce : ResponseMessage
        {
            TryConnect();
            return await _bus.RequestAsync<TRequest, TResponce>(request);
        }

        public IDisposable Respond<TResquest, TResponse>(Func<TResquest, TResponse> responder)
            where TResquest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return _bus.Respond<TResquest, TResponse>(responder);
        }

        public IDisposable RespondAsync<TResquest, TResponse>(Func<TResquest, Task<TResponse>> responder)
            where TResquest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return _bus.RespondAsync<TResquest, TResponse>(responder);
        }

        public void Dispose()
        {
            _bus.Dispose();
        }

        private void TryConnect()
        {
            if (IsConnected) return;

            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(retryCount: 3, sleepDurationProvider: retryAttempt =>
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            policy.Execute(action: () => { 
                    _bus = RabbitHutch.CreateBus(_connectionString);
                //Implementando resiliência, ao desconectar do RabbitMq e conectar novamente
                //Conseguir fazer as subscrições novamente das filas.
                    _advancedBus = _bus.Advanced;
                _advancedBus.Disconnected += OnDisconnect;
                });            
        }

        private void OnDisconnect(object obj, EventArgs eventArgs)
        {
            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .RetryForever();

            policy.Execute(TryConnect);

        }       
    }
}
