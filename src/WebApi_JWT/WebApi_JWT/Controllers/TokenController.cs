using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurityToken.ProviderJWT;
using WebApi_JWT.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_JWT.Controllers
{
    public class TokenController : Controller
    {

        [Route("api/create-token")]
        [AllowAnonymous]
        [HttpPost]
        [Produces("application/json")]
        public IActionResult Token([FromBody] Usuario user)
        {
            /// Postman
            ///http://localhost:64442/api/ListarProdutos
            ///      {
            ///           "name":"carlos",
            ///           "password":"123456",
            ///            "tipo":0
            ///      }
            ///
            ///     tipo 1 UsuarioComum
            ///     tipo 0 Administrador

            if (user.name != "carlos" || user.password != "123456")
                return Unauthorized();

            var token = JwtBearerBuilder.Create(user.tipo);

            return Ok(token.value);
        }
    }
}
