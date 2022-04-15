using Bul.Authority.Application;
using Bul.Authority.Application.DataTranslateObject;
using Bul.Authority.Application.RequestObject;
using Bul.Authority.Entity;
using Bul.Authority.Service;
using Bul.System.Common;
using Bul.System.Extension.NetCore;
using Bul.System.Result;
using Mapster;
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

            var result = this.sqCdbApplication.GetPageListByWhere(pageCondition.Data, pageCondition.PageIndex, pageCondition.PageSize);

            return result;
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("model")]
        public BulResult<SqCdbListDto> GetModel(long id)
        {
            if (id <= 0)
                return BulResult<SqCdbListDto>.Fail(-1, "参数错误");

            //对于简单查询逻辑 可直接在此使用服务层
            var sqCdService = this.HttpContext.GetService<SqCdbService>();

            var model = sqCdService.Db.Query<SqCdb>().FirstOrDefault(f => f.ID == id);

            if (model == null)
                return BulResult<SqCdbListDto>.Fail(-2, "参数错误");

            var result = model.Adapt<SqCdbListDto>();

            if (result.FCDID > 0)
            {
                var parentMenuModel = sqCdService.Db.Query<SqCdb>().FirstOrDefault(f => f.ID == result.FCDID);

                if (parentMenuModel != null)
                    result.FCDMC = parentMenuModel.CDMC;
            }

            return BulResult<SqCdbListDto>.Success(result);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete")]
        public async Task<AbstractResult> CdDelete(EntityIdRo ro)
        {
            if (ro == null || ro.Id <= 0)
                return BulResult.FailNonData(-1, "参数错误");

            var sqCdService = this.HttpContext.GetService<SqCdbService>();

            var model = await sqCdService.Db.Query<SqCdb>().FirstOrDefaultAsync(f => f.ID == ro.Id);
            if (model == null)
                return BulResult.FailNonData(-2, "删除菜单数据错误");

            var efft = await sqCdService.Db.DeleteAsync<SqCdb>(model);
            if (efft > 0)
                return BulResult.SuccessNonData();
            else
                return BulResult.FailNonData(-3, "删除数据失败");
        }
    }
}
