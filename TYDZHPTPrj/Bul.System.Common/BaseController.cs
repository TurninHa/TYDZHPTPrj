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
    [Authorize]
    [ApiController]
    public abstract class BaseController<T> : ControllerBase
    {
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public T CurrentUser { get; protected set; }
    }
}
