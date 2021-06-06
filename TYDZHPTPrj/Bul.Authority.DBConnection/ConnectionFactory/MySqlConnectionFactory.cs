using Chloe.Infrastructure;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.DBConnection.ConnectionFactory
{
    public class MySqlConnectionFactory : IDbConnectionFactory
    {
        private string _;
        public MySqlConnectionFactory(string connectionString)
        {
            _ = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            IDbConnection connection = new MySqlConnection(this._);

            return connection;
        }
    }
}
