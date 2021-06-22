using Bul.Authority.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.System.Common
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public abstract class BaseController<T> : ControllerBase
    {
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public T CurrentUser { get; protected set; }

        /// <summary>
        /// 子类需实现此方法，绑定 CurrentUser
        /// </summary>
        protected abstract void BindCurrentUser();
    }
}
