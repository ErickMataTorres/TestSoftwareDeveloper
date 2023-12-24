using Microsoft.AspNetCore.Mvc;

namespace TestSoftwareDeveloper.Controllers
{
    [ApiController]
    [Route("")]
    public class InicioController : Controller
    {
        [Route("")]
        public IActionResult Inicio()
        {
            return View();
        }
    }
}
