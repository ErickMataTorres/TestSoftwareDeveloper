using System.Data.SqlClient;
using System.Data;

namespace TestSoftwareDeveloper.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Identificacion { get; set; }

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

    }
}
