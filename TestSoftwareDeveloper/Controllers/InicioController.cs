using Microsoft.AspNetCore.Mvc;

namespace TestSoftwareDeveloper.Controllers
{
    // Instrucción para establecer la ruta para la página principal
    [Route("")]
    // **

    // Clase del controlador inicio
    public class InicioController : Controller
    {

        // Instrucción para establecer la ruta para el siguiente método
        [Route("")]
        // **

        // Se regresa la vista inicio
        public IActionResult Inicio()
        {
            return View();
        }
        // **

    }
    // **

}
