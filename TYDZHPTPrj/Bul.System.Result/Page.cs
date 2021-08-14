using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.System.Result
{
    public class Page<T>
    {
        public T Data { get; set; }

        public int PageSize { get; set; } = 20;

        public int PageCount { get; set; }

        public int PageIndex { get; set; }
    }
}
