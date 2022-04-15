using Bul.Authority.Application;
using Bul.Authority.Application.DataTranslateObject;
using Bul.Authority.Application.RequestObject;
using Bul.Authority.Entity;
using Bul.Authority.Service;
using Bul.System.Extension.NetCore;
using Bul.System.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bul.Authority.WebApi.Controllers
{
    public class RoleController : AuthorityBaseController
    {
        private readonly SqRoleApplication SqRoleApplication;
        public RoleController(SqRoleApplication application)
        {
            this.SqRoleApplication = application;
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="ro"></param>
        /// <returns></returns>
        [Route("list")]
        [HttpPost]
        public async Task<BulResult<Page<IEnumerable<SqRoleDto>>>> GetRoleList(RoleRo ro)
        {
            return await this.SqRoleApplication.GetRoleList(ro);
        }

        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="ro"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("save")]
        public async Task<AbstractResult> Save(SaveRoleRo ro)
        {
            return await this.SqRoleApplication.SaveRole(ro);
        }

        /// <summary>
        /// 获取一个Model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("model")]
        public async Task<BulResult<SqRoles>> GetModel(long id)
        {
            if (id <= 0)
                return BulResult<SqRoles>.Fail(-1, "参数错误");

            var roleService = HttpContext.GetService<SqRoleServices>();

            var model = await roleService.Db.Query<SqRoles>().FirstOrDefaultAsync(f => f.ID == id && f.SSGSID == this.CurrentUser.SSGSID);

            if (model == null)
                return BulResult<SqRoles>.Fail(-2, "参数错误");

            return BulResult<SqRoles>.Success(model);
        }

        /// <summary>
        /// 删除一个角色
        /// </summary>
        /// <param name="ro"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete")]
        public async Task<AbstractResult> DeleteRole(EntityIdRo ro)
        {
            if (ro == null || ro.Id <= 0)
                return BulResult.FailNonData(-1, "参数错误");

            return await this.SqRoleApplication.DeleteRole(ro);
        }

        /// <summary>
        /// 禁用 启用 角色
        /// </summary>
        /// <param name="ro">启用禁用参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("disen")]
        public async Task<AbstractResult> DisEnRole(DisEnRoleRo ro)
        {
            if (ro == null || ro.Id <= 0)
                return BulResult.FailNonData(-1, "参数错误");

            var roleService = HttpContext.GetService<SqRoleServices>();

            var model = await roleService.Db.QueryByKeyAsync<SqRoles>(ro.Id);

            model.SYZT = ro.RoleStatue;
            model.Updater = this.CurrentUser.ID;
            model.UpdateTime = DateTime.Now;

            var isSuc = await roleService.Db.UpdateAsync(model);

            if (isSuc > 0)
                return BulResult.SuccessNonData();
            else
                return BulResult.FailNonData(-1, "保存失败");
        }
    }
}
