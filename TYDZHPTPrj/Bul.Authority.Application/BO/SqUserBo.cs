using Bul.Authority.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Application.BO
{
    public class SqUserBo : SqUsers
    {
        public IEnumerable<SqUserRole> SqUserRoles { get; set; }
    }
}
