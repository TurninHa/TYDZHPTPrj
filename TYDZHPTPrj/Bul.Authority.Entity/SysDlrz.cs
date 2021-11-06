using Bul.Entity.Interface;
using Chloe.Annotations;

namespace Bul.Authority.Entity
{
    /// <summary>
    /// 登录日志
    /// </summary>
    [Table("sys_dlrz")]
    public class SysDlrz : IBulEntity
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
