using Bul.Authority.Entity;
using Bul.Authority.Service;
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
        private readonly SqCdbService sqCdbService;
        public SqCdbApplication(SqCdbService cdbService)
        {
            sqCdbService = cdbService;
        }

        public async Task<BulResult<SqCdb>> AddSqCd(SqCdb sqCdb)
        {
            var result = await this.sqCdbService.Save(sqCdb);

            return result;
        }
    }
}
