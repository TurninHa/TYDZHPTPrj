using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Application.RequestObject
{
    public class SaveRoleRo
    {
        public long? ID { get; set; }

        public string JSBM { get; set; }

        public string JSMC { get; set; }

        public int? SYZT { get; set; }
    }
}
