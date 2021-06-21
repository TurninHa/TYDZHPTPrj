using Chloe.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Entity
{
    /// <summary>
    /// 接口权限表
    /// </summary>
    [Table("sq_jkqx")]
    public class SqJkqx
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
        public long CZGNID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string JKDZ { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long SSGSID { get; set; }
    }
}
