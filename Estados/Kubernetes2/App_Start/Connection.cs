using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Kubernetes2.App_Start
{
    public class Connection
    {
        SqlConnection con = new SqlConnection();

        public Connection()
        {
            con.ConnectionString = "Data Source=servprisma.database.windows.net;Initial Catalog=BancoPrisma;User ID=prisma;Password=Y5451711!;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        public SqlConnection Connect()
        {
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro" + e.Message);
            }

            return con;
        }

        public SqlConnection Disconnect()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            return con;
        }
    }
}
