using Bul.Authority.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Application.DataTranslateObject
{
    public class SqRoleUserIdBo : SqRoles
    {
        public long? UserID { get; set; }
    }
}
