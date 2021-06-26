using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Application.RequestObject
{
    public class LoginUserRo
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string ValidateCode { get; set; }
    }
}
