using Bul.Authority.Entity;
using Bul.System.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Application
{
    public class SqCdbApplication
    {
        public async Task<BulResult<SqCdb>> AddSqCd(SqCdb sqCdb)
        {
            return await Task.FromResult(new BulResult<SqCdb>());
        }
    }
}
