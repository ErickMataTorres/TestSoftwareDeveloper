using System.Data;
using System.Data.SqlClient;

namespace TestSoftwareDeveloper.Models
{
    public class Factura
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public double Monto { get; set; }
        public int IdPersona { get; set; }
        public string? NombrePersona { get; set; }
        public string? APaternoPersona { get; set; }
        public string? AMaternoPersona { get; set; }

        public Models.MensajeError Registrar()
        {
            Models.MensajeError mensajeError = new Models.MensajeError();
            SqlConnection conx = Models.ConexionSql.Conectar();
            SqlCommand command = new SqlCommand("spRegistrarFactura", conx);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@Fecha", Fecha);
            command.Parameters.AddWithValue("@Monto", Monto);
            command.Parameters.AddWithValue("@IdPersona", IdPersona);
            conx.Open();
            SqlDataReader dr = command.ExecuteReader();
            try
            {
                if (dr.Read())
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
                if (conx.State == ConnectionState.Open)
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
            SqlCommand command = new SqlCommand("spBorrarFactura", conx);
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

        public static List<Factura> ListaFacturas()
        {
            SqlConnection conx = Models.ConexionSql.Conectar();
            SqlCommand command = new SqlCommand("spListaFacturas", conx);
            command.CommandType = CommandType.StoredProcedure;
            List<Factura> l = new List<Factura>();
            Factura c;
            conx.Open();
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                c = new Factura();
                c.Id = int.Parse(dr["Id"].ToString()!);
                c.Fecha = DateTime.Parse(dr["Fecha"].ToString()!);
                c.Monto = double.Parse(dr["Monto"].ToString()!);
                c.IdPersona = int.Parse(dr["IdPersona"].ToString()!);
                c.NombrePersona = dr["NombrePersona"].ToString();
                c.APaternoPersona = dr["APaternoPersona"].ToString();
                c.AMaternoPersona = dr["AMaternoPersona"].ToString();
                l.Add(c);
            }
            dr.Close();
            conx.Close();
            return l;
        }

    }
}
