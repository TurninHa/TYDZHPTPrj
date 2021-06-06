using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Entity
{
    /// <summary>
    /// 登录日志
    /// </summary>
    public class SysDlrz
    {
        /// <summary>
        /// 
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? KHID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? YHID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string YHM { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DLSJ { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SFCG { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SBYY { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IP { get; set; }
    }
}
