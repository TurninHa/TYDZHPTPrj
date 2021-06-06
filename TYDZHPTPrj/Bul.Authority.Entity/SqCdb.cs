using Chloe.Annotations;
using System;

namespace Bul.Authority.Entity
{
    /// <summary>
    /// 菜单表
    /// </summary>
    [Table("sq_cdb")]
    public class SqCdb
    {
        /// <summary>
        /// ID
        /// </summary>
        [Column(IsPrimaryKey = true)]
        public long ID { get; set; }
        /// <summary>
        /// 菜单编码
        /// </summary>
        public string CDBM { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string CDMC { get; set; }
        /// <summary>
        /// 父菜单ID
        /// </summary>
        public long? FCDID { get; set; }
        /// <summary>
        /// 菜单路径
        /// </summary>
        public string CDLJ { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int PXH { get; set; }
        /// <summary>
        /// 使用状态
        /// </summary>
        public int SYZT { get; set; }
        /// <summary>
        /// 所属公司ID
        /// </summary>
        //[Navigation]
        //[NotMapped()]//不映射任何字段
        public long SSGSID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [UpdateIgnore]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [UpdateIgnore]
        public long? Creater { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public long? Updater { get; set; }
    }
}
