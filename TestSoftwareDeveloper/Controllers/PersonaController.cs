using Microsoft.AspNetCore.Mvc;

namespace TestSoftwareDeveloper.Controllers
{
    // Instrucciones para comunicar que haga la función de una api estableciendo el nombre del controlador como base para la ruta para este controlador
    [ApiController]
    [Route("[controller]")]
    // **

    // Clase del controlador persona
    public class PersonaController : Controller
    {
        // Se asigna el nombre de la ruta para la vista de directorio
        [Route("Directorio")]
        // **

        // Se regresa la vista directorio
        public IActionResult Directorio()
        {
            return View();
        }
        // **

        // Instrucción para indicar que en el siguiente método se hará una petición de tipo GET
        [HttpGet]
        // **

        // Asignación de la ruta de la api para la lista de personas para mostrar al usuario
        [Route("ListaPersonas")]
        // **

        // Lista de personas en formato json que se devuelve para mostrar al usuario
        public IActionResult ListaPersonas()
        {
            var r= Models.Persona.ListaPersonas();
            return Json(r);
        }
        // **

        // Instrucción para indicar que el siguiente método será de tipo POST
        [HttpPost]
        // **

        // Asignación de la ruta de la api para el registro o modificación de una persona
        [Route("RegistrarPersona")]
        // **

        /* Se envían los datos al metodo Registrar de la clase persona para registrar o modificar una persona en la base de datos
           regresando una respuesta en formato json para mostrar al usuario */
        public IActionResult RegistrarPersona(Models.Persona c)
        {
            Models.MensajeError r = c.Registrar();
            return Json(r);
        }
        // **

        // Instrucción para indicar que el siguiente método será de tipo DELETE
        [HttpDelete]
        // **

        // Asignación de la ruta de la api para borrar una persona enviandole el id como parámetro
        [Route("BorrarPersona/{id}")]
        // **

        /* Se envía el id para borrar a la persona con el id correspondiente y se devuelve una respuesta en formato json para
           manejarla y mostrarsela al usuario */
        public IActionResult BorrarPersona(int id)
        {
            string r = Models.Persona.Borrar(id);
            return Json(r);
        }
        // **

    }
    // **

}
