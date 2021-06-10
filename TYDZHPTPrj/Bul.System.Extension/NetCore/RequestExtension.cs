using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.System.Extension.NetCore
{
    public static class RequestExtension
    {
        public static T GetForm<T>(this HttpRequest request, string key)
        {
            if (request == null)
                return default;
            if (string.IsNullOrEmpty(key))
                return default;

            if (!request.Form.Keys.Contains(key))
                return default;

            var fromValue = request.Form[key].ToString();

            T keyValue = (T)Convert.ChangeType(fromValue, typeof(T));

            return keyValue;
        }
    }
}
