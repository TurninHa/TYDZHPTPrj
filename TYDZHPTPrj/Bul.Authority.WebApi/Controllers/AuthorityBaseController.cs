using Bul.Authority.Application;
using Bul.Authority.Entity;
using Bul.System.Common;
using Bul.System.Extension.NetCore;
using Bul.System.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly SqUserApplication userApplication;

        public AuthorityBaseController()
        {
            this.CurrentUser = new SqUsers();
            this.userApplication = this.HttpContext.GetService<SqUserApplication>();
        }
    }
}
