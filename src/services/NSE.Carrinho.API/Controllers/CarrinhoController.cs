using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSE.Carrinho.API.Data;
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
        private readonly CarrinhoContext _context;

        public CarrinhoController(IAspNetUser user, CarrinhoContext context)
        {
            _user = user;
            _context = context;
        }

        [HttpGet("carrinho")]
        public async Task<CarrinhoCliente> ObterCarrinho()
        {
            return await ObterCarrinhoCliente() ?? new CarrinhoCliente();
        }

        [HttpPost("carrinho")]
        public async Task<IActionResult> AdicionarItemCarrinho(CarrinhoItem carrinhoItem)
        {
            var carrinho = await ObterCarrinhoCliente();

            if (carrinho == null)
                ManipularNovoCarrinho(carrinhoItem);
            else
                ManipularCarrinhoExistente(carrinho, carrinhoItem);

            if (!OperacaoValida()) return CustomResponse();

            var result = await _context.SaveChangesAsync();
            if (result <= 0) AdicionarErroProcessamento("Não foi possível persistir os dados no banco.");

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

        private async Task<CarrinhoCliente> ObterCarrinhoCliente()
        {
            return await _context.CarrinhoCliente
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.ClienteId == _user.ObterUserId());
        }

        private void ManipularNovoCarrinho(CarrinhoItem carrinhoItem)
        {
            var carrinho = new CarrinhoCliente(_user.ObterUserId());
            carrinho.AdicionarItem(carrinhoItem);

            _context.CarrinhoCliente.Add(carrinho);
        }

        private void ManipularCarrinhoExistente(CarrinhoCliente carrinhoCliente, CarrinhoItem carrinhoItem)
        {
            var produtoItemExistente = carrinhoCliente.CarrinhoItemExistente(carrinhoItem);

            carrinhoCliente.AdicionarItem(carrinhoItem);

            if (produtoItemExistente)            
                _context.CarrinhoItens.Update(carrinhoCliente.ObterProdutoId(carrinhoItem.ProdutoId));
            else            
                _context.CarrinhoItens.Add(carrinhoItem);
            
            _context.CarrinhoCliente.Update(carrinhoCliente);
        }
    }
}
