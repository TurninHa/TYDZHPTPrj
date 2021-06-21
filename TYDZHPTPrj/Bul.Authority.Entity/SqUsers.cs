using Chloe.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Entity
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [Table("sq_users")]
    public class SqUsers
    {
        /// <summary>
        /// 
        /// </summary>
        [Column(IsPrimaryKey = true)]
        [AutoIncrement]
        public long ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string YHM { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string XM { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MM { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SJH { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long SSYGID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long SSGSID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SFGLY { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SYZT { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SFSC { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? Creater { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? Updater { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdateTime { get; set; }
    }
}
