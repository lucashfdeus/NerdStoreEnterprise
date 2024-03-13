using Microsoft.Extensions.DependencyInjection;
using NSE.Catalogo.API.Data.Repositoty;
using NSE.Catalogo.API.Data;
using NSE.Catalogo.API.Model;

namespace NSE.Catalogo.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<CatalagoContext>();
        }
    }
}
