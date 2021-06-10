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
    }

    public class BulResult<T> : AbstractResult where T : class, new()
    {
        public new T Data { get; protected set; }

        public static BulResult<T> Fail(int code)
        {
            return new BulResult<T> { Code = code, Message = string.Empty, Data = default };
        }

        public static BulResult<T> Fail(int code, string message)
        {
            return new BulResult<T> { Code = code, Message = message, Data = default };
        }

        public static BulResult<T> Fail(int code, string message, string extensionData)
        {
            return new BulResult<T> { Code = code, Message = message, Data = default, ExtensionData = extensionData };
        }

        public static BulResult<T> Fail(int code, string message, string extensionData, T data)
        {
            return new BulResult<T> { Code = code, Message = message, Data = data, ExtensionData = extensionData };
        }

        public static BulResult<T> Success()
        {
            return new BulResult<T>() { Code = 0, Message = string.Empty, Data = default, ExtensionData = string.Empty };
        }

        public static BulResult<T> Success(string message)
        {
            return new BulResult<T>() { Code = 0, Message = message, Data = default, ExtensionData = string.Empty };
        }

        public static BulResult<T> Success(string message, string extensionData)
        {
            return new BulResult<T>() { Code = 0, Message = message, Data = default, ExtensionData = extensionData };
        }

        public static BulResult<T> Success(T data, string extensionData = "")
        {
            return new BulResult<T>() { Code = 0, Message = string.Empty, Data = data, ExtensionData = extensionData };
        }

        public static BulResult<T> Success(string message, T data, string extensionData = "")
        {
            return new BulResult<T>() { Code = 0, Message = message, Data = data, ExtensionData = extensionData };
        }
    }
}
