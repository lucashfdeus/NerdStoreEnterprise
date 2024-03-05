using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Controllers
{
    public class IdentidadeController : Controller
    {
        [HttpGet]
        [Route("nova-conta")]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [Route("nova-conta")]
        public async Task<IActionResult> Registro(UsuarioRegistro usuarioRegistro)
        {
            if (!ModelState.IsValid) return View(usuarioRegistro);

            //Conectar na API para registro de usuário.s

            if (false) return View(usuarioRegistro);

            //Realizar login na APP

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UsuarioLogin usuarioLogin)
        {
            if (!ModelState.IsValid) return View(usuarioLogin);

            //Conectar na API para login do usuário.s

            if (false) return View(usuarioLogin);

            //Realizar login na APP

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [Route("sair")]
        public async Task<IActionResult> Logout()
        {

            return RedirectToActionPermanent("Index", "Home");
        }
    }
}
