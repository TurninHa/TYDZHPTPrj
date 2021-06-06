using Chloe;
using Chloe.Infrastructure;
using Chloe.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.DBConnection.AuthorityMySqlDbContext
{
    public class AuthorityDbContext : MySqlContext
    {
        public AuthorityDbContext(IDbConnectionFactory factory) : base(factory)
        {

        }
    }
}
