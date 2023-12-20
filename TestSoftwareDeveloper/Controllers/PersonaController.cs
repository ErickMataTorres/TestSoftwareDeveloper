using Microsoft.AspNetCore.Mvc;

namespace TestSoftwareDeveloper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonaController : Controller
    {
        [Route("Directorio")]
        public IActionResult Directorio()
        {
            return View();
        }
        [HttpGet]
        [Route("ListaPersonas")]
        public IActionResult ListaPersonas()
        {
            var r= Models.Persona.ListaPersonas();
            return Json(r);
        }
        [HttpPost]
        [Route("RegistrarPersona")]
        public IActionResult RegistrarPersona(Models.Persona c)
        {
            Models.MensajeError r = c.Registrar();
            return Json(r);
        }
        [HttpDelete]
        [Route("BorrarPersona/{id}")]
        public IActionResult BorrarPersona(int id)
        {
            string r = Models.Persona.Borrar(id);
            return Json(r);
        }
    }
}
