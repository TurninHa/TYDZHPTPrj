using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
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

        public static TCurrentUser GetCurrentUser<TCurrentUser>(this HttpContext httpContext)
        {
            if (httpContext == null)
                return default;

            if (httpContext == null || httpContext.Items == null || httpContext.Items.Count == 0)
                return default;

            var keyName = "CurrentUser"; //nameof(TCurrentUser);
            if (!httpContext.Items.ContainsKey(keyName))
                return default;

            var result = JsonConvert.DeserializeObject<TCurrentUser>(httpContext.Items[keyName].ToString());

            if (result == null)
                return default;

            return result;
        }
    }
}
