using Bul.Authority.Application.RequestObject;
using Bul.Authority.Entity;
using Bul.Authority.Service;
using Bul.System.Result;
using Mapster;
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
        public SqCzgnApplication(SqCzgnServices sqCzgn)
        {
            this.SqCzgnService = sqCzgn;
        }

        public async Task<AbstractResult> Save(CdczgnRo ro)
        {
            if (ro == null)
                return BulResult.FailNonData(-1, "参数错误");

            var entity = ro.Adapt<SqCdczgn>();

            if (entity.ID > 0)
                entity.UpdateTime = DateTime.Now;
            else
                entity.CreateTime = DateTime.Now;

            return await this.SqCzgnService.Save(entity);
        }
    }
}
