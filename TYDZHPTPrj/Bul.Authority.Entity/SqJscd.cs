using Bul.Entity.Interface;
using Chloe.Annotations;
using System;

namespace Bul.Authority.Entity
{
    /// <summary>
    /// 角色菜单表
    /// </summary>
    [Table("sq_jscd")]
    public class SqJscd : IBulEntity
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
        public long RoleID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long CDID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long SSGSID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? Creater { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? Updater { get; set; }
    }
}
