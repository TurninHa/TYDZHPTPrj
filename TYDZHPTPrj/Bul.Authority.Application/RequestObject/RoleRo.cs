using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Application.RequestObject
{
    public class RoleRo
    {
        /// <summary>
        /// 角色编码
        /// </summary>
        public string JSBM { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string JSMC { get; set; }
        /// <summary>
        /// 使用状态
        /// </summary>
        public int? SYZT { get; set; }

        /// <summary>
        /// 所属公司ID
        /// </summary>
        public long? SSGSID { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
