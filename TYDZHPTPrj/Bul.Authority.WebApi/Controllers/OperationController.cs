using Bul.Authority.Application;
using Bul.Authority.Application.RequestObject;
using Bul.Authority.Entity;
using Bul.Authority.Service;
using Bul.System.Extension.NetCore;
using Bul.System.Result;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bul.Authority.WebApi.Controllers
{
    public class OperationController : AuthorityBaseController
    {
        private readonly SqCzgnApplication SqCzgnApp;
        public OperationController(SqCzgnApplication sqCzgnApplication) : base()
        {
            SqCzgnApp = sqCzgnApplication;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="ro"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("save")]
        public async Task<AbstractResult> Save(CdczgnRo ro)
        {
            return await SqCzgnApp.Save(ro);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="cdId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public BulResult<IEnumerable<CdczgnRo>> GetList(int cdId)
        {
            return this.SqCzgnApp.GetList(cdId, 0);
        }

        /// <summary>
        /// 删除操作功能
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete")]
        public AbstractResult DeleteCzgn(EntityIdRo ro)
        {
            if (ro == null || ro.Id <= 0)
                return BulResult.FailNonData(-1, "参数错误");

            var SqCzgnService = HttpContext.GetService<SqCzgnServices>();

            var model = SqCzgnService.Db.Query<SqCdczgn>().Where(w => w.ID == ro.Id).FirstOrDefault();

            if (model == null)
                return BulResult.FailNonData(-1, "参数错误");

            var result = SqCzgnService.Db.DeleteByKey<SqCdczgn>(ro.Id);

            if (result > 0)
                return BulResult.SuccessNonData();

            return BulResult.FailNonData(-2, "删除失败");
        }
    }
}
