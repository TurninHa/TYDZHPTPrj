using Bul.Authority.DBConnection.AuthorityMySqlDbContext;
using Bul.System.Extension.NetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Bul.Authority.DBConnection.DbContextBaseService
{
    public class BaseService
    {
        protected AuthorityDbContext Db;
        protected readonly IHttpContextAccessor HttpContextAccessor;


        public BaseService(IHttpContextAccessor contextAccessor)
        {
            this.HttpContextAccessor = contextAccessor;

            this.Db = this.HttpContextAccessor.GetService<AuthorityDbContext>();
        }

        protected T GetService<T>()
        {
            return this.HttpContextAccessor.GetService<T>();
        }
    }
}
