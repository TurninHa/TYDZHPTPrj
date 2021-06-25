using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.System.Extension.NetCore
{
    public static class HttpContextAccessorExtension
    {
        /// <summary>
        /// 获取服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contextAccessor"></param>
        /// <returns></returns>
        public static T GetService<T>(this IHttpContextAccessor contextAccessor)
        {
            if (contextAccessor == null)
                return default;

            return contextAccessor.HttpContext.RequestServices.GetService<T>();
        }

        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        /// <typeparam name="TCurrentUser"></typeparam>
        /// <param name="contextAccessor"></param>
        /// <returns></returns>
        public static TCurrentUser GetCurrentUser<TCurrentUser>(this IHttpContextAccessor contextAccessor)
        {
            if (contextAccessor == null)
                return default;

            if (contextAccessor.HttpContext == null || contextAccessor.HttpContext.Items == null || contextAccessor.HttpContext.Items.Count == 0)
                return default;

            var keyName = nameof(TCurrentUser);
            if (!contextAccessor.HttpContext.Items.ContainsKey(keyName))
                return default;

            var result = contextAccessor.HttpContext.Items[keyName];

            if (result == null)
                return default;

            return (TCurrentUser)result;
        }
    }
}
