using Bul.Authority.DBConnection.AuthorityMySqlDbContext;
using Bul.System.Extension.NetCore;
using Chloe;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Bul.Authority.DBConnection.DbContextBaseService
{
    public abstract class BaseService
    {
        public IDbContext DbContext
        {
            get
            {
                return this.HttpContextAccessor.GetService<IDbContext>();
            }
        }

        protected abstract IHttpContextAccessor HttpContextAccessor
        {
            get;
        }

        protected T GetService<T>()
        {
            return this.HttpContextAccessor.GetService<T>();
        }
    }
}
