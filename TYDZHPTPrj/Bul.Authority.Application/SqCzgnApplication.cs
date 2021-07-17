using Bul.Authority.Application.RequestObject;
using Bul.Authority.Entity;
using Bul.Authority.Service;
using Bul.System.Extension.NetCore;
using Bul.System.Result;
using Mapster;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Application
{
    public class SqCzgnApplication
    {
        private readonly SqCzgnServices SqCzgnService;
        private readonly IHttpContextAccessor HttpContextAccessor;
        public SqCzgnApplication(SqCzgnServices sqCzgn, IHttpContextAccessor contextAccessor)
        {
            this.SqCzgnService = sqCzgn;
            this.HttpContextAccessor = contextAccessor;
        }

        public async Task<AbstractResult> Save(CdczgnRo ro)
        {
            if (ro == null)
                return BulResult.FailNonData(-1, "参数错误");

            var entity = ro.Adapt<SqCdczgn>();

            if (entity.ID > 0)
            {
                entity.Updater = HttpContextAccessor.HttpContext.GetCurrentUser<SqUsers>().ID;
                entity.UpdateTime = DateTime.Now;
            }
            else
            {
                entity.Creater = HttpContextAccessor.HttpContext.GetCurrentUser<SqUsers>().ID;
                entity.CreateTime = DateTime.Now;
            }

            return await this.SqCzgnService.Save(entity);
        }

        public BulResult<IEnumerable<CdczgnRo>> GetList(long cdId, long ssgsId)
        {
            if (cdId <= 0)
                return BulResult<IEnumerable<CdczgnRo>>.Fail(-1, "请选择菜单并传入菜单Id");

            var query = this.SqCzgnService.Db.Query<SqCdczgn>();

            query = query.Where(w => w.CDID == cdId);
            if (ssgsId > 0)
                query = query.Where(w => (w.SSGSID == ssgsId || w.SSGSID == 0));

            var result = query.ToList();
            var dtoList = result.Adapt<IEnumerable<CdczgnRo>>();

            return BulResult<IEnumerable<CdczgnRo>>.Success(dtoList);

        }
    }
}
