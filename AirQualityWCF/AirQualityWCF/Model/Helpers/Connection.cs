using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirQualityWCF.Model.Helpers
{
    public class Connection
    {
        public SqlConnection SqlConnection { get; set; }

        readonly string _connectionString =
                "Data Source=192.168.1.41,1433;Initial Catalog=AirQuality;" +
                "Integrated Security=False;User ID=testConn;" +
                "Password=test1234;Connect Timeout=30;Encrypt=False;" +
                "TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";

        public Connection()
        {
            SqlConnection = new SqlConnection(_connectionString);
        }
    }
}
