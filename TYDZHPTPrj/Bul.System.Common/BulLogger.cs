using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.System.Common
{
    public class BulLogger
    {
        private BulLogger() { }

        private static string _loggerName = "Bul.Common.Logger";
        private static readonly ILog Logger = null;

        static BulLogger()
        {
            Logger = LogManager.GetLogger(_loggerName);
        }

        /// <summary>
        /// 获取一个指定类型的Log实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ILog GetLogger<T>()
        {
            return LogManager.GetLogger(typeof(T));
        }

        public static void Info(object content)
        {
            Debug.WriteLine(content.ToString());
            Logger.Info(content);
        }

        public static void Warn(object content)
        {
            Console.WriteLine(content.ToString());
            Logger.Warn(content);
        }

        public static void Error(object content)
        {
            Console.WriteLine(content.ToString());
            Logger.Error(content);
        }

        public static void Fatal(object content)
        {
            Console.WriteLine(content.ToString());
            Logger.Fatal(content);
        }
    }
}
