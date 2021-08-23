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

            //this.BindCurrentUser();
        }

        protected override void BindCurrentUser()
        {
            if (this.User == null) return;

            var loginSqUser = this.User.Claims.FirstOrDefault(f => f.Type == "sub")?.Value;
            if (string.IsNullOrEmpty(loginSqUser))
                this.HttpContext.Response.WriteAsJsonAsync(BulResult.FailNonData(-10, "非法登录"));

            var userEntity = JsonConvert.DeserializeObject<SqUsers>(loginSqUser);

            if (userEntity == null || string.IsNullOrEmpty(userEntity.YHM) || string.IsNullOrEmpty(userEntity.SJH))
                this.HttpContext.Response.WriteAsJsonAsync(BulResult.FailNonData(-10, "非法登录"));

            var bulResult = this.userApplication.GetUserModelByYhmAndSjh(userEntity.YHM, userEntity.SJH);

            if (bulResult.Code == 0)
                this.CurrentUser = bulResult.Data;
            else
                this.HttpContext.Response.WriteAsJsonAsync(BulResult.FailNonData(-10, "非法登录"));

            this.HttpContext.Items.Add("CurrentUser", this.CurrentUser);
        }
    }
}
