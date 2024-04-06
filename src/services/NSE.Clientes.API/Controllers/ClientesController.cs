using Microsoft.AspNetCore.Mvc;
using NSE.Clientes.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.WebAPI.Core.Controllers;
using System;
using System.Threading.Tasks;

namespace NSE.Clientes.API.Controllers
{
    public class ClientesController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ClientesController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet("Clientes")]
        public async Task<IActionResult> Index()
        {
           var resultado = await _mediatorHandler.EnviarComando(new RegistrarClienteCommand
                (
                    id: Guid.NewGuid(),
                    nome: "Lucas Henrique",
                    email: "teste@teste.com",
                    cpf: "04703379155"
                ));
            
            return CustomResponse(resultado);
        }
    }
}
