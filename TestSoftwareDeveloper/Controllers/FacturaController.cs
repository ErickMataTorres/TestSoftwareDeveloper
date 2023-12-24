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
        [HttpPost]
        [Route("RegistrarFactura")]
        public IActionResult RegistrarFactura(Models.Factura c)
        {
            Models.MensajeError r = c.Registrar();
            return Json(r);
        }
        [HttpDelete]
        [Route("BorrarFactura/{id}")]
        public IActionResult BorrarFactura(int id)
        {
            string r = Models.Factura.Borrar(id);
            return Json(r);
        }
    }
}
