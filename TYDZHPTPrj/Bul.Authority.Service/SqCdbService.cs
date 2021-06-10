using Bul.Authority.DBConnection.DbContextBaseService;
using Bul.Authority.Entity;
using Bul.System.Result;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Service
{
    public class SqCdbService : BaseService
    {

        public SqCdbService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        { }

        public async Task<BulResult<SqCdb>> Add(SqCdb cdb)
        {
            var result = await this.Db.InsertAsync(cdb);

            return BulResult<SqCdb>.Success(result);
        }


    }
}
