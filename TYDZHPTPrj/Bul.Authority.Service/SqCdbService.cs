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
            if (cdb == null)
                return BulResult<SqCdb>.Fail(-1, "参数错误");

            if (string.IsNullOrEmpty(cdb.CDBM))
                return BulResult<SqCdb>.Fail(-2, "菜单编码不能为空");

            if (string.IsNullOrEmpty(cdb.CDMC))
                return BulResult<SqCdb>.Fail(-3, "菜单名称不能为空");

            if (cdb.FCDID == null)
                return BulResult<SqCdb>.Fail(-3, "父级菜单Id不能为空");

            var result = await this.Db.InsertAsync(cdb);

            if (result == null)
                return BulResult<SqCdb>.Fail(-4, "添加失败");

            return BulResult<SqCdb>.Success(result);
        }


    }
}
