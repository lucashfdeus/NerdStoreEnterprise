using System;
using System.Collections.Generic;
using System.Linq;

namespace NSE.Carrinho.API.Model
{
    public class CarrinhoCliente
    {
        internal const int MAX_QUANTIDADE_ITEM = 5; 

        public CarrinhoCliente(Guid clienteId)
        {
            Id = Guid.NewGuid();
            ClienteId = clienteId;

        }

        public CarrinhoCliente() { }

        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public decimal ValorTotal { get; set; }

        public List<CarrinhoItem> Items { get; set; } = new List<CarrinhoItem>();

        internal void CalcularValorCarrinho()
        {
            ValorTotal = Items.Sum(p => p.CalcularValor());
        }

        internal bool CarrinhoItemExistente(CarrinhoItem item)
        {
            return Items.Any(p => p.ProdutoId == item.ProdutoId);
        }

        internal CarrinhoItem ObterProdutoId(Guid produtoId)
        {
            return Items.FirstOrDefault(p => p.ProdutoId == produtoId);
        }

        internal void AdicionarItem(CarrinhoItem item)
        {
            //TODO: Criar refatoração para retornar as msgs de validações para o cliente.
            if(!item.EhValido()) return;

            item.AssociarCarrinho(Id);

            if (CarrinhoItemExistente(item))
            {
                var itemExistente = ObterProdutoId(item.ProdutoId);
                itemExistente.AdicionarUnidades(item.Quantidade);

                item = itemExistente;
                Items.Remove(itemExistente);
            }

            Items.Add(item);
            
            CalcularValorCarrinho();
        }
    }
}
