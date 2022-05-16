using Bul.Authority.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Application.DataTranslateObject
{
    public class SqUsersDto : SqUsers
    {
        /// <summary>
        /// 所属员工姓名
        /// </summary>
        public string SSYGXM { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleNames { get; set; }

        /// <summary>
        /// 使用状态名称
        /// </summary>
        public string SYZTName { get; set; }

        /// <summary>
        /// 是否删除名称
        /// </summary>
        public string SFSCName { get; set; }

        /// <summary>
        /// 所属公司名称
        /// </summary>
        public string SSGSName { get; set; }
    }
}
