using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.System.Extension.NetCore
{
    public static class HttpContextExtension
    {
        public static T GetService<T>(this HttpContext httpContext)
        {
            if (httpContext == null)
                return default;

            return httpContext.RequestServices.GetService<T>();
        }
    }
}
