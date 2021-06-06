using Bul.Authority.DBConnection.AuthorityMySqlDbContext;
using Chloe.MySql;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IHttpContextAccessor HttpContextAccessor;


        public BaseService(IHttpContextAccessor contextAccessor)
        {
            this.HttpContextAccessor = contextAccessor;

            this.Db = this.HttpContextAccessor.HttpContext.RequestServices.GetService<AuthorityDbContext>();
        }

        protected T GetService<T>()
        {
            return this.HttpContextAccessor.HttpContext.RequestServices.GetService<T>();
        }
    }
}
