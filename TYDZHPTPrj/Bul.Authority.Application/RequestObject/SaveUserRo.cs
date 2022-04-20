using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Application.RequestObject
{
    public class SaveUserRo
    {
        public long ID { get; set; }
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
        public string MM { get; set; }
        
        public string QRMM { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string SJH { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long SSYGID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long SSGSID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SFGLY { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SYZT { get; set; }

        public IEnumerable<long> Roles { get; set; }
    }
}
