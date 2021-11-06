using Chloe.Annotations;
using System;
using Bul.Entity.Interface;

namespace Bul.Authority.Entity
{
    /// <summary>
    /// 客户-菜单表
    /// </summary>
    [Table("sq_khcdb")]
    public class SqKHCDB : IBulEntity
    {
        [Column(IsPrimaryKey = true)]
        [AutoIncrement]
        public long ID { get; set; }

        public long CDID { get; set; }

        public long SSGSID { get; set; }

        public string CDBieM { get; set; }

        public DateTime? CreateTime { get; set; }

        public long? Creater { get; set; }

        public DateTime? UpdateTime { get; set; }

        public long? Updater { get; set; }

    }
}