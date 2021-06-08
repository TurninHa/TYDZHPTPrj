using System;
using System.Collections.Generic;
using System.Linq;
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

        public object ExtensionData { get; set; }

        public abstract AbstractResult Success();

        public abstract AbstractResult Success(string message);

        public abstract AbstractResult Success(string message, object data);

        public abstract AbstractResult SuccessForExtension(string message, object extensionData);

        public abstract AbstractResult SuccessForExtension(string message, object data, object extensionData);


        public abstract AbstractResult Fail(int code, string message);

        public abstract AbstractResult Fail(int code, string message, object data);

        public abstract AbstractResult FailForExtension(int code, string message, object extensionData);

        public abstract AbstractResult FailForExtension(int code, string message, object data, object extensionData);
    }

    public class BulResult<T> : AbstractResult where T : class, new()
    {
        public override AbstractResult Fail(int code, string message)
        {
            throw new NotImplementedException();
        }

        public override AbstractResult Fail(int code, string message, object data)
        {
            throw new NotImplementedException();
        }

        public override AbstractResult FailForExtension(int code, string message, object extensionData)
        {
            throw new NotImplementedException();
        }

        public override AbstractResult FailForExtension(int code, string message, object data, object extensionData)
        {
            throw new NotImplementedException();
        }

        public override AbstractResult Success()
        {
            throw new NotImplementedException();
        }

        public override AbstractResult Success(string message)
        {
            throw new NotImplementedException();
        }

        public override AbstractResult Success(string message, object data)
        {
            throw new NotImplementedException();
        }

        public override AbstractResult SuccessForExtension(string message, object extensionData)
        {
            throw new NotImplementedException();
        }

        public override AbstractResult SuccessForExtension(string message, object data, object extensionData)
        {
            throw new NotImplementedException();
        }
    }
}
