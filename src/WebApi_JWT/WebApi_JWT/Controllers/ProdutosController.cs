using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi_JWT.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_JWT.Controllers
{

    [Authorize(Policy = "TotalAccess")]
    public class ProdutosController : Controller
    {
        [HttpGet]
        [Route("api/ListarProdutos")]
        public IActionResult ListarProdutos()
        {

            var lista = new List<Produto>();

            for (int i = 1; i <= 10; i++)
            {
                lista.Add(new Produto { Id = i, NomeProduto = "Nome produto - " + i.ToString() });
            }

            return Ok(lista);
        }
    }
}
