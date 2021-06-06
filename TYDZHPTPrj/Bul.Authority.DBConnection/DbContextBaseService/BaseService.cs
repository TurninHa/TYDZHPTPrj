using Bul.Authority.DBConnection.AuthorityMySqlDbContext;
using Chloe.MySql;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.DBConnection.DbContextBaseService
{
    public class BaseService
    {
        protected AuthorityDbContext Db;
        public BaseService(IHttpContextAccessor contextAccessor)
        {

        }
    }
}
