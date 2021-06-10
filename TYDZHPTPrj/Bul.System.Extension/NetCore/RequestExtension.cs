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
        /// <summary>
        /// 获取一个key对应的一个值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetForm<T>(this HttpRequest request, string key)
        {
            if (request == null)
                return default;
            if (string.IsNullOrEmpty(key))
                return default;

            if (!request.Form.Keys.Contains(key))
                return default;

            var formValue = request.Form[key];

            T keyValue;

            if (formValue.Count == 1)
                keyValue = (T)Convert.ChangeType(formValue.First(), typeof(T));
            else
                return default;

            return keyValue;
        }

        /// <summary>
        /// 获取一个key中多个值。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetFormForList<T>(this HttpRequest request, string key)
        {
            if (request == null)
                return default;
            if (string.IsNullOrEmpty(key))
                return default;

            if (!request.Form.Keys.Contains(key))
                return default;

            var formValue = request.Form[key];

            IList<T> result = new List<T>();

            if (formValue.Count >= 1)
            {
                foreach (var item in formValue)
                {
                    result.Add((T)Convert.ChangeType(item, typeof(T)));
                }
            }
            else
                return default;

            return result;
        }
    }
}
