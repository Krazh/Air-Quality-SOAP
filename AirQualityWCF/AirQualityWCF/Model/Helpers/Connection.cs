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
                "Server=tcp:mortensql.database.windows.net,1433;Database=AirQuality;" +
                "User ID=sqladmin@mortensql;Password=sortTelefon1;Encrypt=True;" +
                "TrustServerCertificate=False;";

        public Connection()
        {
            SqlConnection = new SqlConnection(_connectionString);
        }
    }
}
