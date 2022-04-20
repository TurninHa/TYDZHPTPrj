using Bul.Authority.Application.ApplicationBase;
using Bul.Authority.Application.RequestObject;
using Bul.Authority.Entity;
using Bul.Authority.Service;
using Bul.System.Extension.NetCore;
using Bul.System.Result;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Application
{
    public class SqUserApplication : BulAuthorityApplication
    {
        private readonly SqUsersService UsersServices;
        public SqUserApplication(SqUsersService services)
        {
            this.UsersServices = services;
        }

        public BulResult<SqUsers> GetUserModelByYhmAndSjh(string yhm, string sjh)
        {
            if (string.IsNullOrEmpty(yhm))
                return BulResult<SqUsers>.Fail(-1, "用户名不能为空");

            var query = this.UsersServices.DbContext.Query<SqUsers>();
            query = query.Where(w => w.YHM == yhm && w.SJH == sjh);


            var model = query?.FirstOrDefault();
            if (model == null)
                return BulResult<SqUsers>.Fail(-2, "未检索到数据");

            return BulResult<SqUsers>.Success(model);
        }

        public BulResult<SqUsers> ValidateUserLogin(LoginUserRo userRo)
        {
            if (userRo == null)
                return BulResult<SqUsers>.Fail(-1, "参数错误");

            if (string.IsNullOrEmpty(userRo.UserName) || string.IsNullOrEmpty(userRo.Password))
                return BulResult<SqUsers>.Fail(-2, "请输入用户名密码");

            var query = this.UsersServices.DbContext.Query<SqUsers>();
            query = query.Where(w => w.YHM == userRo.UserName && w.MM == userRo.Password && w.SFSC == 0 && w.SYZT == 1);//TODO 处理加密


            var model = query.FirstOrDefault();
            if (model == null)
                return BulResult<SqUsers>.Fail(-3, "用户名密码错误");

            var userRoleService = this.HttpContextAccessor.GetService<SqUserRoleApplication>();
            var userRoles = userRoleService.GetUserRoleByUserId(model.ID, model.SSGSID);

            if (userRoles.Code != 0 || (userRoles.Code == 0 && !userRoles.Data.Any()))
                return BulResult<SqUsers>.Fail(-4, "当前用户没有设置角色");

            return BulResult<SqUsers>.Success(model);
        }

        public async Task<AbstractResult> SaveUser(SaveUserRo userRo)
        {
            if (userRo == null)
                return BulResult.FailNonData(-1, "参数错误");

            if (string.IsNullOrEmpty(userRo.YHM))
                return BulResult.FailNonData(-2, "请输入用户名");

            if (string.IsNullOrEmpty(userRo.XM))
                return BulResult.FailNonData(-3, "请输入姓名");

            if (string.IsNullOrEmpty(userRo.MM) || string.IsNullOrEmpty(userRo.QRMM) || userRo.MM != userRo.QRMM)
                return BulResult.FailNonData(-4, "两次密码输入不一致");


            userRo.SSGSID = this.CurrentLoginUser.SSGSID;//所属公司Id为当前登录用户的公司ID


            if (this.CurrentLoginUser.SSGSID != 0)//对于客户必须选择所属员工
            {
                if (userRo.SSYGID <= 0)
                    return BulResult.FailNonData(-5, "请选择所属员工");
            }

            return BulResult.FailNonData(-6, "");
        }
    }
}
