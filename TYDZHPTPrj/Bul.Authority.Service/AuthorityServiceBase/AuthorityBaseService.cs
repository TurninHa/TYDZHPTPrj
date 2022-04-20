using Bul.Authority.DBConnection.DbContextBaseService;
using Bul.Authority.Entity;
using Bul.Authority.Entity.ExtObj;
using Bul.System.Common;
using Bul.System.Extension.NetCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Service.AuthorityServiceBase
{
    public class AuthorityBaseService : BaseService
    {
        protected override IHttpContextAccessor HttpContextAccessor
        {
            get
            {
                return BulAppIoc.GetService<IHttpContextAccessor>();
            }
        }
        
        protected SqLoginUser CurrentLoginUser
        {
            get
            {
                return this.HttpContextAccessor.GetCurrentUser<SqLoginUser>();
            }
        }
    }
}
