using Bul.Authority.Application.ApplicationBase;
using Bul.Authority.Application.DataTranslateObject;
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


        public async Task<AbstractResult> SaveRole(SaveRoleRo roleRo)
        {
            
        }
    }
}
