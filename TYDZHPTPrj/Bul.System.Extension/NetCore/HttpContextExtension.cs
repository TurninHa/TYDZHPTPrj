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

            if (httpContext.Items == null || httpContext.Items.Count == 0)
                return default;

            var keyName = nameof(TCurrentUser);

            if (httpContext.Items.ContainsKey(keyName))
                return (TCurrentUser)httpContext.Items[keyName];

            var ul = httpContext.User.Claims.FirstOrDefault(f => f.Type == "ul")?.Value;

            if (string.IsNullOrEmpty(ul))
                return default;

            var result = JsonConvert.DeserializeObject<TCurrentUser>(ul);
            if (result == null)
                return default;

            httpContext.Items.Add(keyName, result);

            return result;
        }
    }
}
