using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.System.Common
{
    public static class BulAppIoc
    {
        private static IApplicationBuilder ApplicationBuilder { get; set; }

        public static IApplicationBuilder AppServiceExtention(this IApplicationBuilder app)
        {
            ApplicationBuilder = app;

            return app;
        }

        public static T GetService<T>()
        {
            return ApplicationBuilder.ApplicationServices.GetService<T>();
        }
    }
}
