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
                l.Add(c);
            }
            dr.Close();
            conx.Close();
            return l;
        }

    }
}
