using Bul.Authority.Application.ApplicationBase;
using Bul.Authority.Application.DataTranslateObject;
using Bul.Authority.Application.RequestObject;
using Bul.Authority.Entity;
using Bul.Authority.Service;
using Bul.System.Extension.NetCore;
using Bul.System.Result;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Application
{
    public class SqRoleApplication : BulAuthorityApplication
    {
        private readonly SqRoleServices _services;
        public SqRoleApplication(SqRoleServices sqRoleServices)
        {
            this._services = sqRoleServices;
        }

        /// <summary>
        /// 分页获取角色列表
        /// 支持客户查看角色 客户只加载自己的角色
        /// </summary>
        /// <param name="roleRo"></param>
        /// <returns></returns>
        public async Task<BulResult<Page<IEnumerable<SqRoleDto>>>> GetRoleList(RoleRo roleRo)
        {
            if (roleRo == null)
                return BulResult<IEnumerable<SqRoleDto>>.PageFail(-1, "参数错误");

            var query = this._services.Db.Query<SqRoles>();

            if (!string.IsNullOrEmpty(roleRo.JSBM))
                query = query.Where(r => r.JSBM == roleRo.JSBM);

            if (!string.IsNullOrEmpty(roleRo.JSMC))
                query = query.Where(r => r.JSMC == roleRo.JSMC);

            if (roleRo.SYZT != null)
                query = query.Where(r => r.SYZT == roleRo.SYZT);

            if (this.LoginUser.SSGSID != 0)//说明是客户操作的
            {
                query = query.Where(r => r.SSGSID == this.LoginUser.SSGSID);
            }
            else
            {
                //此时说明不是客户操作
                if (roleRo.SSGSID != null)
                    query = query.Where(r => r.SSGSID == roleRo.SSGSID);
            }

            var count = await query.CountAsync();
            var pageResult = await query.TakePage(roleRo.PageIndex, roleRo.PageSize).ToListAsync();

            var result = pageResult.Adapt<List<SqRoleDto>>();

            foreach (var item in result)
            {
                switch (item.SYZT)
                {
                    case 1:
                        item.StatusName = "正在使用";//使用状态名称
                        break;
                    case 0:
                        item.StatusName = "已停用";//使用状态名称
                        break;
                }
            }

            return BulResult<IEnumerable<SqRoleDto>>.PageSuccess(result, roleRo.PageIndex, roleRo.PageSize, count);
        }

        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="saveRoleRo"></param>
        /// <returns></returns>

        public async Task<AbstractResult> SaveRole(SaveRoleRo saveRoleRo)
        {
            if (this.LoginUser.SFGLY == 0)
                return BulResult<AbstractResult>.Fail(-3, "非管理员不能增加角色");

            if (saveRoleRo == null)
                return BulResult<AbstractResult>.Fail(-1, "参数错误");

            if (string.IsNullOrEmpty(saveRoleRo.JSBM) || string.IsNullOrEmpty(saveRoleRo.JSMC))
                return BulResult<AbstractResult>.Fail(-1, "参数错误");

            var query = this._services.Db.Query<SqRoles>();

            query = query.Where(w => w.JSBM == saveRoleRo.JSBM && w.SSGSID == this.LoginUser.SSGSID && w.SFSC == 0);

            if (saveRoleRo.ID != null && saveRoleRo.ID > 0)
                query = query.Where(w => w.ID != saveRoleRo.ID);

            var model = query.FirstOrDefault();

            if (model != null)
                return BulResult<AbstractResult>.Fail(-2, "角色编码已存在");

            if (saveRoleRo.ID != null && saveRoleRo.ID > 0)
            {
                var addModel = saveRoleRo.Adapt<SqRoles>();

                if (addModel == null)
                    return BulResult<AbstractResult>.Fail(-4, "转换数据失败");

                addModel.Creater = this.LoginUser.ID;
                addModel.CreateTime = DateTime.Now;

                addModel.SSGSID = this.LoginUser.SSGSID;
                addModel.SFSC = 0;

                var newAddModel = await this._services.Db.InsertAsync(addModel);

                if (newAddModel == null || newAddModel.ID <= 0)
                    return BulResult<AbstractResult>.Fail(-5, "保存失败");

                return BulResult.SuccessNonData();
            }
            else
            {
                var updateModel = await this._services.Db.Query<SqRoles>().FirstOrDefaultAsync(f => f.ID == saveRoleRo.ID && f.SSGSID == this.LoginUser.SSGSID);

                updateModel.Updater = this.LoginUser.ID;
                updateModel.UpdateTime = DateTime.Now;

                updateModel.SYZT = saveRoleRo.SYZT.Value;
                updateModel.JSMC = saveRoleRo.JSMC;

                var isSuc = await this._services.Db.UpdateAsync(updateModel);

                if (isSuc <= 0)
                    return BulResult<AbstractResult>.Fail(-5, "更新失败");

                return BulResult.SuccessNonData();
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ro"></param>
        /// <returns></returns>
        public async Task<AbstractResult> DeleteRole(DeleteByIdRo ro)
        {
            if (ro == null || ro.Id <= 0)
                return BulResult.FailNonData(-1, "参数错误");

            var model = await this._services.Db.Query<SqRoles>().FirstOrDefaultAsync(f => f.ID == ro.Id && f.SSGSID == this.LoginUser.SSGSID);

            if (model == null)
                return BulResult.FailNonData(-2, "未检索到数据");

            var userRoleService = HttpContextAccessor.GetService<SqUserRoleService>();

            var userRoleCount = await userRoleService.Db.Query<SqUserRole>().Where(w => w.RoleID == model.ID && w.SSGSID == this.LoginUser.SSGSID).CountAsync();

            if (userRoleCount > 0)
                return BulResult.FailNonData(-3, "当前角色正在被使用，不能删除");

            model.SFSC = 1;
            model.Updater = this.LoginUser.ID;
            model.UpdateTime = DateTime.Now;

            var isSuc = await this._services.Db.DeleteAsync(model);

            if (isSuc > 0)
                return BulResult.SuccessNonData();

            return BulResult.FailNonData(-10, "删除失败");
        }
    }
}
