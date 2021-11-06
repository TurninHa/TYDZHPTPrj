using Chloe.Annotations;
using System;
using Bul.Entity.Interface;

namespace Bul.Authority.Entity
{
    /// <summary>
    /// 角色操作功能表
    /// </summary>
    [Table("sq_jsczgn")]
    public class SqJsczgn : IBulEntity
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
        public long JSID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long CDID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long CZGNID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long SSGSID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? Creater { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
