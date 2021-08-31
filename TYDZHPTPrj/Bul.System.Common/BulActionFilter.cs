using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.System.Common
{
    public class BulActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (context.HttpContext.User == null) return;

            var loginSqUser = context.HttpContext.User.Claims.FirstOrDefault(f => f.Type == "ul")?.Value;

            context.HttpContext.Items.Add("CurrentUser", loginSqUser);
        }
    }
}
