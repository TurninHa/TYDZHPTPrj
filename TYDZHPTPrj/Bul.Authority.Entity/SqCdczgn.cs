using Chloe.Annotations;
using System;
using Bul.Entity.Interface;

namespace Bul.Authority.Entity
{
    /// <summary>
    /// 菜单操作功能表
    /// </summary>
    [Table("sq_cdczgn")]
    public class SqCdczgn : IBulEntity
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
        public long CDID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string GNBM { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string GNMC { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PXH { get; set; }
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
