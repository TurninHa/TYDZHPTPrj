using Bul.Authority.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Application.DataTranslateObject
{
    public class SqRoleDto : SqRoles
    {
        /// <summary>
        /// 使用状态名称
        /// </summary>
        public string StatusName { get;set; }
    }
}
