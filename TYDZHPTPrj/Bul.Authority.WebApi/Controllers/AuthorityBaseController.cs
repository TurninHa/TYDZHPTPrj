using Bul.Authority.Application;
using Bul.Authority.Application.BO;
using Bul.Authority.Entity;
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
        public override SqUsers CurrentUser
        {
            get
            {
                return this.HttpContext.GetCurrentUser<SqUserBo>();
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
