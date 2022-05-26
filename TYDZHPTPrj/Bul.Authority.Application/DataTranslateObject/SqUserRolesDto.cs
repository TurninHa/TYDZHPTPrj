using Bul.Authority.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Application.DataTranslateObject
{
    public class SqUserRolesDto : SqUsers
    {
        /// <summary>
        /// 用户所属角色
        /// </summary>
        public IEnumerable<SqRoles> Roles { get; set; }
    }

}
