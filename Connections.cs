using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sajidan_ukk_dekstop
{
    internal class Connections
    {
        public static string url = "Data Source=MYPCPRO\\SQLSERVER;Initial Catalog=sajidan__ukk;Integrated Security=True;TrustServerCertificate=True";
        public static SqlConnection koneksi;
        public static SqlConnection Connect()
        {
            if (koneksi == null)
            {
                koneksi = new SqlConnection(url);
            }
            return koneksi;
        }
    }
}
