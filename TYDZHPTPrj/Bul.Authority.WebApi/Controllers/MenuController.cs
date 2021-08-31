using Bul.Authority.Application;
using Bul.Authority.Application.DataTranslateObject;
using Bul.Authority.Entity;
using Bul.System.Common;
using Bul.System.Extension.NetCore;
using Bul.System.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Bul.Authority.WebApi.Controllers
{
    public class MenuController : AuthorityBaseController
    {
        private readonly SqCdbApplication sqCdbApplication;
        public MenuController(SqCdbApplication cdbApp) : base()
        {
            sqCdbApplication = cdbApp;
        }


        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="cdb"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("save")]
        public async Task<AbstractResult> Save(SqCdb cdb)
        {
            var vo = await this.sqCdbApplication.SaveSqCd(cdb);
            if (vo.Code == 0 && vo.Data.ID > 0)
                return BulResult<long>.Success(vo.Data.ID);
            else
                return BulResult.FailNonData(vo.Code, vo.Message);
        }

        [HttpGet]
        [Route("childmenu")]
        public async Task<BulResult<IEnumerable<SqCdb>>> GetMenuByFcdId(long fcdid)
        {
            var result = await this.sqCdbApplication.GetChildMenuList(fcdid);

            return result;
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("menutree")]
        public BulResult<IEnumerable<SqCdbDto>> GetSqCdTree()
        {
            var treeResult = this.sqCdbApplication.GetSqCdbTree(this.CurrentUser.SSGSID);

            return treeResult;
        }

        /// <summary>
        /// 根据条件获取列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("list")]
        public BulResult<IEnumerable<SqCdbListDto>> GetList(SqCdbListDto condition)
        {
            return this.sqCdbApplication.GetListByWhere(condition);
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageCondition"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("page")]
        public BulResult<Page<IEnumerable<SqCdbListDto>>> GetListPage(Page<SqCdbListDto> pageCondition)
        {
            if (pageCondition == null)
                return BulResult<IEnumerable<SqCdbListDto>>.PageFail(-1, "参数错误");

            if (pageCondition.PageIndex == 0)
                pageCondition.PageIndex = 1;

            if (pageCondition.PageSize == 0)
                pageCondition.PageSize = 20;

            Debug.WriteLine(HttpContext.GetCurrentUser<SqUsers>().YHM);

            var result = this.sqCdbApplication.GetPageListByWhere(pageCondition.Data, pageCondition.PageIndex, pageCondition.PageSize);

            return result;
        }
    }
}
