using Microsoft.AspNetCore.Mvc;

namespace TestSoftwareDeveloper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FacturaController : Controller
    {
        [Route("Ventas")]
        public IActionResult Ventas()
        {
            return View();
        }
        [HttpGet]
        [Route("ListaFacturas")]
        public IActionResult ListaFacturas()
        {
            var r = Models.Factura.ListaFacturas();
            return Json(r);
        }
    }
}
