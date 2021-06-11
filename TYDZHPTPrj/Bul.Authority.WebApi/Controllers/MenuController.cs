using Bul.Authority.Application;
using Bul.Authority.Entity;
using Bul.System.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bul.Authority.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly SqCdbApplication sqCdbApplication;
        public MenuController(SqCdbApplication cdbApp)
        {
            sqCdbApplication = cdbApp;
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="cdb"></param>
        /// <returns></returns>
        public async Task<AbstractResult> Save(SqCdb cdb)
        {
            if (cdb.ID == 0)
            {
                var vo = await this.sqCdbApplication.AddSqCd(cdb);
                if (vo.Code == 0 && vo.Data.ID > 0)
                    return BulResult<long>.Success(vo.Data.ID);
                else
                    return BulResult.FailNonData(vo.Code, vo.Message);
            }

            return BulResult.FailNonData(-10, "暂不支持");
        }
    }
}
