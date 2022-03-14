using Bul.Authority.Application.BO;
using Bul.Authority.Entity;
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

        public SqUsers LoginUser
        {
            get
            {
                return this.HttpContextAccessor.GetCurrentUser<SqUserBo>();
            }
        }
    }
}
