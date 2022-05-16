using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Application.RequestObject
{
    public class UserListRo: EntityBaseRo
    {
        /// <summary>
        /// 
        /// </summary>
        public string YHM { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string XM { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SJH { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? SSGSID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? SYZT { get; set; }
    }
}
