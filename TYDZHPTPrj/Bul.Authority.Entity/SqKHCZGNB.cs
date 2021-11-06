using Chloe.Annotations;
using System;
using Bul.Entity.Interface;

namespace Bul.Authority.Entity
{
    /// <summary>
    /// 客户-操作功能表
    /// </summary>
    [Table("sq_khczgnb")]
    public class SqKHCZGNB : IBulEntity
    {
        [Column(IsPrimaryKey = true)]
        [AutoIncrement]
        public long ID { get; set; }

        public long GNID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long SSGSID { get; set; }

        public DateTime? CreateTime { get; set; }

        public long? Creater { get; set; }

        public DateTime? UpdateTime { get; set; }

        public long? Updater { get; set; }


    }
}