using System.Data.SqlClient;
using System.Data;

namespace TestSoftwareDeveloper.Models
{

    // Clase Persona
    public class Persona
    {

        // Propiedades para manejar la información de la persona
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Identificacion { get; set; }
        // **

        // Se manda llamar este método ya sea para registrar o modificar una persona en la base de datos a través de un procedimiento almacenado
        public Models.MensajeError Registrar()
        {
            Models.MensajeError mensajeError = new Models.MensajeError();
            SqlConnection conx = Models.ConexionSql.Conectar();
            SqlCommand command = new SqlCommand("spRegistrarPersona", conx);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@Nombre", Nombre);
            command.Parameters.AddWithValue("@ApellidoPaterno", ApellidoPaterno);
            command.Parameters.AddWithValue("@ApellidoMaterno", ApellidoMaterno);
            command.Parameters.AddWithValue("@Identificacion", Identificacion);
            conx.Open();
            SqlDataReader dr = command.ExecuteReader();
            try
            {
                if(dr.Read())
                {
                    mensajeError.Id = int.Parse(dr["Id"].ToString()!);
                    mensajeError.Nombre = dr["Nombre"].ToString();
                }
                dr.Close();
                conx.Close();
            }
            catch(Exception error)
            {
                mensajeError.Nombre = error.Message;
                if(conx.State== ConnectionState.Open)
                {
                    dr.Close();
                    conx.Close();
                }
            }
            return mensajeError;
        }
        // **

        // Se envía id para borrar una persona en la base de datos a través de un procedimiento almacenado
        public static string Borrar(int id)
        {
            string respuesta = string.Empty;
            SqlConnection conx = Models.ConexionSql.Conectar();
            SqlCommand command = new SqlCommand("spBorrarPersona", conx);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);
            try
            {
                conx.Open();
                respuesta = command.ExecuteScalar().ToString()!;
                conx.Close();
            }
            catch (Exception error)
            {
                respuesta = error.Message;
                if (conx.State == ConnectionState.Open)
                {
                    conx.Close();
                }
            }
            return respuesta;
        }
        // **

        /* Se pide la información de la tabla personas a través de un procedimiento almacenado y se crea una lista que se devuelve en formato json para
           mostrarselo al usuario */
        public static List<Persona> ListaPersonas()
        {
            SqlConnection conx = Models.ConexionSql.Conectar();
            SqlCommand command = new SqlCommand("spListaPersonas", conx);
            command.CommandType= CommandType.StoredProcedure;
            List<Persona> l = new List<Persona>();
            Persona c;
            conx.Open();
            SqlDataReader dr= command.ExecuteReader();
            while (dr.Read())
            {
                c = new Persona();
                c.Id = int.Parse(dr["Id"].ToString()!);
                c.Nombre = dr["Nombre"].ToString();
                c.ApellidoPaterno = dr["ApellidoPaterno"].ToString();
                c.ApellidoMaterno = dr["ApellidoMaterno"].ToString();
                c.Identificacion = dr["Identificacion"].ToString();
                l.Add(c);
            }
            dr.Close();
            conx.Close();
            return l;
        }
        // **

    }
    // **

}
