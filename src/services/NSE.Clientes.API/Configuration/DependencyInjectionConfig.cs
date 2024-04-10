using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NSE.Clientes.API.Application.Commands;
using NSE.Clientes.API.Application.Events;
using NSE.Clientes.API.Data;
using NSE.Clientes.API.Data.Repository;
using NSE.Clientes.API.Models;
using NSE.Core.Mediator;

namespace NSE.Clientes.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            //Commands
            services.AddScoped<IRequestHandler<RegistrarClienteCommand, ValidationResult>, ClienteCommandHandler>();

            //Events
            services.AddScoped<INotificationHandler<ClienteRegistradoEvent>, ClienteEventHandler>();

            //Repository
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ClientesContext>();


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
