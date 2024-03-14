using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Services;
using NSE.WebApp.MVC.Services.Handlers;
using System;

namespace NSE.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>(); //Ativando o delegate para interceptação

            //Registro API
            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();

           // services.AddHttpClient<ICatalogoService, CatalogoService>()
           //     .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>(); //Fazendo interceptação de qualquer request desse serviço.

            services.AddHttpClient("Refit", options =>
            {
                options.BaseAddress = new Uri(configuration.GetSection("CatalogoUrl").Value);
            })
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            .AddTypedClient(Refit.RestService.For<ICatalogoServiceRefit>); //Usando Refit 

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUser, AspNetUser>();
        }
    }
}
