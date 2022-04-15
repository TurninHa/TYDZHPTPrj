using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Application.RequestObject
{
    public class DisEnRoleRo : EntityIdRo
    {
        /// <summary>
        /// 启用 1 禁用 0
        /// </summary>
        public int RoleStatue { get; set; }
    }
}
