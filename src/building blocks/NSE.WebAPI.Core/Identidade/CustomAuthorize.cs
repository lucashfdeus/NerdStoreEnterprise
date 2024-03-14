using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace NSE.WebAPI.Core.Identidade
{
    public class CustomAuthorize
    {
        public static bool ValidarClaimUsuario(HttpContext context, string claimName, string claimValue)
        {
            return context.User.Identity.IsAuthenticated &&
                context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
        }
    }

    public class ClaimsAuthorizeAttribute : TypeFilterAttribute 
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RegistroClaimFilter)) 
        { 
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }

    }

    public class RegistroClaimFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;

        public RegistroClaimFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(401); //Não foi autenticado.
                return;
            }

            if (!CustomAuthorize.ValidarClaimUsuario(context.HttpContext, _claim.Type, _claim.Value))
            {
                context.Result = new StatusCodeResult(403); // Foi autenticado mas não tem permissão.
            }
        }
    }
}
