using Bul.Authority.Application;
using Bul.Authority.Application.RequestObject;
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
    }
}
