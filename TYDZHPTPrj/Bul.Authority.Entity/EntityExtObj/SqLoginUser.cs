using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Entity.ExtObj
{
    public class SqLoginUser : SqUsers
    {
        public IEnumerable<SqUserRole> SqUserRoles { get; set; }
    }
}
