using Bul.Authority.Application;
using Bul.Authority.Entity;
using Bul.Authority.Entity.ExtObj;
using Bul.System.Common;
using Bul.System.Extension.NetCore;
using Bul.System.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Digests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bul.Authority.WebApi.Controllers
{

    public class AuthorityBaseController : BaseController<SqUsers>
    {
        public override SqLoginUser CurrentUser
        {
            get
            {
                return this.HttpContext.GetCurrentUser<SqLoginUser>();
            }
        }

        public ILogger<AuthorityBaseController> Logger
        {
            get
            {
                return this.HttpContext.GetService<ILogger<AuthorityBaseController>>();
            }
        }
    }
}
