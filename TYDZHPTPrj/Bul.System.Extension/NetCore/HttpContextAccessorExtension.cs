﻿using Microsoft.AspNetCore.Http;
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
        public static T GetService<T>(this IHttpContextAccessor contextAccessor)
        {
            if (contextAccessor == null)
                return default;

            return contextAccessor.HttpContext.RequestServices.GetService<T>();
        }
    }
}