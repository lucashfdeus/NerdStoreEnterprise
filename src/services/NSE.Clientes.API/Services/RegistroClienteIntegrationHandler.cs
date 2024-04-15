using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSE.Clientes.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.Core.Messages.Integration;
using NSE.MessageBus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NSE.Clientes.API.Services
{
    public class RegistroClienteIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _messageBus;
        private readonly IServiceProvider _serviceProvider;       

        public RegistroClienteIntegrationHandler(IServiceProvider serviceProvider, IMessageBus messageBus)
        {
            _serviceProvider = serviceProvider;
            _messageBus = messageBus;
        }
        private void SetRespondAsync()
        {
            _messageBus.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(async request =>
               await RegistrarCliente(request));

            _messageBus.AdvancedBus.Connected += OnConnect;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetRespondAsync();
            return Task.CompletedTask;
        }       

        private void OnConnect(object obj, EventArgs eventArgs)
        {
            SetRespondAsync();
        }

        private async Task<ResponseMessage> RegistrarCliente(UsuarioRegistradoIntegrationEvent message)
        {
            var clienteCommand = new RegistrarClienteCommand(message.Id, message.Nome, message.Email, message.Cpf);
            ValidationResult sucesso;
            
            //ServiceLocator - é a msm coisa da injeção de dependencia.
            //devido ao pipeline no asp.net ser singleton e não scoped.
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                sucesso = await mediator.EnviarComando(clienteCommand);
            }

            return new ResponseMessage(sucesso);
        }
    }
}
