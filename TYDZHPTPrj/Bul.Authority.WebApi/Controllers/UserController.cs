using Bul.Authority.Application;
using Bul.Authority.Application.RequestObject;
using Bul.System.Common;
using Bul.System.Extension.NetCore;
using Bul.System.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bul.Authority.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SqUserApplication sqUserApplication;
        public UserController(SqUserApplication userApplication)
        {
            this.sqUserApplication = userApplication;
        }

        [HttpPost]
        [Route("login")]
        public AbstractResult UserLogin(LoginUserRo loginUser)
        {
            try
            {
                var loginResult = this.sqUserApplication.ValidateUserLogin(loginUser);
                if (loginResult.Code != 0)
                    return loginResult;

                var jwtBearer = this.HttpContext.GetService<JwtBearer>();
                var token = jwtBearer.LoginToken(loginResult.Data, 8);

                BulLogger.Info("登录成功");

                return BulResult<string>.Success(token);
            }
            catch (Exception er)
            {
                return BulResult.FailNonData(-10, er.Message);
            }
        }
    }
}
