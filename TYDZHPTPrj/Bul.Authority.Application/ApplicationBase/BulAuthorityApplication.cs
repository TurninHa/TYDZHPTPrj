using Bul.Authority.Entity;
using Bul.Authority.Entity.ExtObj;
using Bul.Entity.Interface;
using Bul.System.Common;
using Bul.System.Extension.NetCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Application.ApplicationBase
{
    public class BulAuthorityApplication : BulApplication
    {
        public override IHttpContextAccessor HttpContextAccessor
        {
            get
            {
                return BulAppIoc.GetService<IHttpContextAccessor>();
            }
        }

        public SqLoginUser CurrentLoginUser
        {
            get
            {
                return this.HttpContextAccessor.GetCurrentUser<SqLoginUser>();
            }
        }
    }
}
