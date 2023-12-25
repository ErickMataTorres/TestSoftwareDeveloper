using Microsoft.AspNetCore.Mvc;

namespace TestSoftwareDeveloper.Controllers
{

    // Instrucciones para comunicar que haga la función de una api estableciendo el nombre del controlador como base para la ruta para este controlador
    [ApiController]
    [Route("[controller]")]
    // **

    // Clase del controlador factura
    public class FacturaController : Controller
    {
        // Se asigna el nombre de la ruta para la vista de ventas
        [Route("Ventas")]
        // **

        // Se regresa la vista ventas
        public IActionResult Ventas()
        {
            return View();
        }
        // **

        // Instrucción para indicar que en el siguiente método se hará una petición de tipo GET
        [HttpGet]
        // **

        // Asignación de la ruta de la api para la lista de facturas para mostrar al usuario
        [Route("ListaFacturas")]
        // **

        // Lista de facturas en formato json que se devuelve para mostrar al usuario
        public IActionResult ListaFacturas()
        {
            var r = Models.Factura.ListaFacturas();
            return Json(r);
        }
        // **

        // Instrucción para indicar que el siguiente método será de tipo POST
        [HttpPost]
        // **

        // Asignación de la ruta de la api para el registro o modificación de una factura
        [Route("RegistrarFactura")]
        // **

        /* Se envían los datos al metodo Registrar de la clase factura para registrar o modificar una factura en la base de datos
           regresando una respuesta en formato json para mostrar al usuario */
        public IActionResult RegistrarFactura(Models.Factura c)
        {
            Models.MensajeError r = c.Registrar();
            return Json(r);
        }
        // **

        // Instrucción para indicar que el siguiente método será de tipo DELETE
        [HttpDelete]
        // **

        // Asignación de la ruta de la api para borrar una factura enviandole el id como parámetro
        [Route("BorrarFactura/{id}")]
        // **

        /* Se envía el id para borrar la factura con el id correspondiente y se devuelve una respuesta en formato json para
           manejarla y mostrarsela al usuario */
        public IActionResult BorrarFactura(int id)
        {
            string r = Models.Factura.Borrar(id);
            return Json(r);
        }
        // **

    }
    // **

}
