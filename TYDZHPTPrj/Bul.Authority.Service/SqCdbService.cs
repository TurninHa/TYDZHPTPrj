using Bul.Authority.DBConnection.DbContextBaseService;
using Bul.Authority.Entity;
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

        public async Task<long> Add(SqCdb cdb)
        {
            var result = await this.Db.InsertAsync(cdb);

            return result.ID;
        }
    }
}
