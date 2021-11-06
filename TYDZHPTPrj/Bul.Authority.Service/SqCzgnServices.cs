using Bul.Authority.DBConnection.DbContextBaseService;
using Bul.Authority.Entity;
using Bul.System.Result;
using Chloe;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Service
{
    public class SqCzgnServices : BaseService
    {
        public SqCzgnServices(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        { }

        public async Task<AbstractResult> Save(SqCdczgn entity)
        {
            if (entity.CDID <= 0)
                return BulResult.FailNonData(-1, "参数错误CDID");

            if (string.IsNullOrEmpty(entity.GNBM))
                return BulResult.FailNonData(-2, "功能编码不能为空");

            if (string.IsNullOrEmpty(entity.GNMC))
                return BulResult.FailNonData(-3, "功能名称不能为空");

            var isExistQueryGnbm = this.Db.Query<SqCdczgn>();
            isExistQueryGnbm = isExistQueryGnbm.Where(w => w.GNBM == entity.GNBM);
            isExistQueryGnbm = isExistQueryGnbm.Where(w => w.CDID == entity.CDID);

            if (entity.ID > 0)
                isExistQueryGnbm = isExistQueryGnbm.Where(w => w.ID != entity.ID);

            var isExistGnbnList = await isExistQueryGnbm.ToListAsync();

            if (isExistGnbnList != null && isExistGnbnList.Any())
                return BulResult.FailNonData(-4, "功能编码已存在");

            if (entity.ID <= 0)
            {
                var isSuc = await this.Db.InsertAsync(entity);
                if (isSuc != null && isSuc.ID > 0)
                    return BulResult.SuccessNonData("保存成功", isSuc.ID.ToString());
                else
                    return BulResult.FailNonData(-8, "保存失败");
            }
            else
            {
                var isSuc = await this.Db.UpdateAsync(entity);

                if (isSuc > 0)
                    return BulResult.SuccessNonData("更新成功");
                else
                    return BulResult.FailNonData(-8, "更新失败");
            }
        }
    }
}
