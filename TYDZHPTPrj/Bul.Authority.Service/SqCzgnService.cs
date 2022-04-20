using Bul.Authority.DBConnection.DbContextBaseService;
using Bul.Authority.Entity;
using Bul.Authority.Service.AuthorityServiceBase;
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
    public class SqCzgnService : AuthorityBaseService
    {
        public async Task<AbstractResult> Save(SqCdczgn entity)
        {
            if (entity.CDID <= 0)
                return BulResult.FailNonData(-1, "参数错误CDID");

            if (string.IsNullOrEmpty(entity.GNBM))
                return BulResult.FailNonData(-2, "功能编码不能为空");

            if (string.IsNullOrEmpty(entity.GNMC))
                return BulResult.FailNonData(-3, "功能名称不能为空");

            var isExistQueryGnbm = this.DbContext.Query<SqCdczgn>();
            isExistQueryGnbm = isExistQueryGnbm.Where(w => w.GNBM == entity.GNBM && w.CDID == entity.CDID);

            if (entity.ID > 0)
                isExistQueryGnbm = isExistQueryGnbm.Where(w => w.ID != entity.ID);

            var isExistGnbnList = await isExistQueryGnbm.FirstOrDefaultAsync();

            if (isExistGnbnList != null)
                return BulResult.FailNonData(-4, "功能编码已存在");

            if (entity.ID <= 0)
            {
                var isSuc = await this.DbContext.InsertAsync(entity);
                if (isSuc != null && isSuc.ID > 0)
                    return BulResult.SuccessNonData("保存成功", isSuc.ID.ToString());
                else
                    return BulResult.FailNonData(-8, "保存失败");
            }
            else
            {
                var oriEntity = await this.DbContext.Query<SqCdczgn>().FirstOrDefaultAsync(f => f.ID == entity.ID);

                entity.CreateTime = oriEntity.CreateTime;
                entity.Creater = oriEntity.Creater;

                var isSuc = await this.DbContext.UpdateAsync(entity);

                if (isSuc > 0)
                    return BulResult.SuccessNonData("更新成功");
                else
                    return BulResult.FailNonData(-8, "更新失败");
            }
        }
    }
}
