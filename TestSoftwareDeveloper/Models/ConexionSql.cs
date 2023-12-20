using System.Data.SqlClient;

namespace TestSoftwareDeveloper.Models
{
    public class ConexionSql
    {
        public static SqlConnection Conectar()
        {
            //string conx = "DATA SOURCE = A; INITIAL CATALOG = TestSoftwareDeveloper; INTEGRATED SECURITY = YES";
            string conx = "SERVER = TestSoftwareDeveloper.mssql.somee.com; DATABASE = TestSoftwareDeveloper; USER ID = Abcdefghij110_SQLLogin_1; PASSWORD = jrhjvpetvh;";
            SqlConnection s=new SqlConnection(conx);
            return s;
        }
    }
}
