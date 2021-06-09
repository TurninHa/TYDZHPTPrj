using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Bul.System.Result
{
    /// <summary>
    /// 默认Code = 0 执行成功
    /// </summary>
    public abstract class AbstractResult
    {
        public int Code { get; protected set; }

        public string Message { get; protected set; }

        public object Data { get; protected set; }

        public string ExtensionData { get; set; }

        /// <summary>
        /// 以0标识 被调用的方法正确执行了
        /// </summary>
        public int ResultSuccess { get; private set; } = 0;
    }

    public class BulResult<T> : AbstractResult where T : class, new()
    {
        public new T Data { get; protected set; }

        public static BulResult<T> Fail(int code, string message = "", T data = default)
        {
            return new BulResult<T> { Code = code, Message = message, Data = data };
        }

        public static BulResult<T> Fail(int code, string extensionData, string message = "", T data = default)
        {
            return new BulResult<T> { Code = code, Message = message, Data = data, ExtensionData = extensionData };
        }


        public static BulResult<T> Success(string message = "", T data = default, string extensionData = "")
        {
            return new BulResult<T>() { Code = 0, Message = message, Data = data, ExtensionData = extensionData };
        }
    }
}
