using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Carrinho.API.Model;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.Usuario;
using System;
using System.Threading.Tasks;

namespace NSE.Carrinho.API.Controllers
{
    [Authorize]
    public class CarrinhoController : MainController
    {
        private readonly IAspNetUser _user;

        public CarrinhoController(IAspNetUser user)
        {
            _user = user;
        }

        [HttpGet("carrinho")]
        public async Task<CarrinhoCliente> ObterCarrinho()
        {
            return null;
        }

        [HttpPost("carrinho")]
        public async Task<IActionResult> AdicionarItemCarrinho(CarrinhoItem carrinhoItem)
        {
            return CustomResponse();
        }

        [HttpPut("carrinho")]
        public async Task<IActionResult> AtualizarItemCarrinho(Guid produtoId, CarrinhoItem carrinhoItem)
        {
            return CustomResponse();
        }

        [HttpDelete("carrinho/{produtoId}")]
        public async Task<IActionResult> RemoverItemCarrinho(Guid produtoId)
        {
            return CustomResponse();
        }
    }
}
