using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using System.Linq;

namespace NSE.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool ResponsePossuiErros(ResponseResult result)
        {
            if (result != null && result.Erros.Mensagens.Any())
            {
                return true;
            }

            return false;
        }
    }
}
