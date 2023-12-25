using System.Data.SqlClient;

namespace TestSoftwareDeveloper.Models
{
    public class ConexionSql
    {
        // Método para conectar a la base de datos SQL Server enviadole la cadena de conexión
        public static SqlConnection Conectar()
        {
            //string conx = "DATA SOURCE = A; INITIAL CATALOG = TestSoftwareDeveloper; INTEGRATED SECURITY = YES";
            string conx = "SERVER = TestSoftwareDeveloper.mssql.somee.com; DATABASE = TestSoftwareDeveloper; USER ID = Abcdefghij110_SQLLogin_1; PASSWORD = jrhjvpetvh;";
            SqlConnection s=new SqlConnection(conx);
            return s;
        }
        // **
    }
}
