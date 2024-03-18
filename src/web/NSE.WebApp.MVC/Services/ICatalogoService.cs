using NSE.WebApp.MVC.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Services
{
    public interface ICatalogoService
    {
       
        Task<IEnumerable<ProdutoViewModel>> ObterTodos();

        Task<ProdutoViewModel> ObterPorId(Guid id);
    }

    //NÃO ESTAMOS MAIS USANDO ESSA INTERFACE DO REFIT
    public interface ICatalogoServiceRefit
    {
        [Get("/catalogo/produtos/")]
        Task<IEnumerable<ProdutoViewModel>> ObterTodos();

        [Get("/catalogo/produtos/{id}")]
        Task<ProdutoViewModel> ObterPorId(Guid id);
    }
}

