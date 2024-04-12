using NSE.Core.Messages.Integration;
using System;
using System.Threading.Tasks;

namespace NSE.MessageBus
{
    public interface IMessageBus : IDisposable
    {
        void Publish<T>(T message) where T : IntegrationEvent;

        Task PublishAsync<T>(T message) where T : IntegrationEvent;

        void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class;

        void SubscribeAsync<T>(string subscriptionID, Func<T, Task> onMessage) where T : class;

        TResponce Request<TRequest, TResponce>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponce : ResponseMessage;

        Task<TResponce> RequestAsync<TRequest, TResponce>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponce : ResponseMessage;

        IDisposable Respond<TResquest, TResponse>(Func<TResquest, TResponse> responder)
             where TResquest : IntegrationEvent
             where TResponse : ResponseMessage;

        IDisposable RespondAsync<TResquest, TResponse>(Func<TResquest, Task<TResponse>> responder)
             where TResquest : IntegrationEvent
             where TResponse : ResponseMessage;

        bool IsConnected { get; }
    }
}
