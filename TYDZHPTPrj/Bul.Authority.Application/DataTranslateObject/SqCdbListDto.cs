using Chloe.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Application.DataTranslateObject
{
    public class SqCdbListDto
    {
        /// <summary>
        /// ID
        /// </summary>
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
        public int? PXH { get; set; }
        /// <summary>
        /// 使用状态
        /// </summary>
        public int SYZT { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 父菜单名称
        /// </summary>
        public string FCDMC { get; set;  }
    }
}
