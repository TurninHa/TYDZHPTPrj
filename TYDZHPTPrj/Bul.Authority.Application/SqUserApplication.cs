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

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="userRo"></param>
        /// <returns></returns>
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

            if (string.IsNullOrEmpty(userRo.SJH))
                return BulResult.FailNonData(-4, "手机号不能为空");

            if (userRo.Roles == null || !userRo.Roles.Any())
                return BulResult.FailNonData(-4, "请选择用户所属角色");

            var userRoleService = HttpContextAccessor.GetService<SqUserRoleService>();

            if (userRo.Id > 0)
            {
                var userModel = await this.UsersServices.DbContext.QueryByKeyAsync<SqUsers>(userRo.Id);
                if (userModel == null)
                    return BulResult.FailNonData(-6, "未检索到数据");

                userModel.XM = userRo.XM;
                userModel.SSYGID = userRo.SSYGID;
                userModel.SFGLY = userRo.SFGLY;
                userModel.SJH = userRo.SJH;
                userModel.SYZT = userRo.SYZT;

                userModel.Updater = this.CurrentLoginUser.ID;
                userModel.UpdateTime = DateTime.Now;

                var isSucc = await this.UsersServices.DbContext.UpdateAsync(userModel);
                if (isSucc <= 0)
                    return BulResult.FailNonData(-7, "修改失败");

                var isDelSuc = await userRoleService.DbContext.DeleteAsync<SqUserRole>(w => w.UserID == userRo.Id && w.SSGSID == this.CurrentLoginUser.SSGSID);

                if (isDelSuc > 0)
                {
                    await userRoleService.AddUserRole(userRo.Id, userRo.Roles);

                    return BulResult.SuccessNonData();
                }
                else
                    return BulResult.FailNonData(-8, "保存用户信息失败");
            }
            else
            {
                var isExistQuery = UsersServices.DbContext.Query<SqUsers>();

                isExistQuery = isExistQuery.Where(w => w.YHM == userRo.YHM && w.SFSC == 0);//此处只检查没有删除的用户

                var yhCount = await isExistQuery.CountAsync();

                if (yhCount > 0)
                    return BulResult.FailNonData(-9, "用户名已被使用");

                var userModel = new SqUsers
                {
                    YHM = userRo.YHM,
                    XM = userRo.XM,
                    MM = userRo.QRMM,
                    SJH = userRo.SJH,
                    SSYGID = userRo.SSYGID,
                    SSGSID = this.CurrentLoginUser.SSGSID,
                    SFGLY = userRo.SFGLY,
                    SYZT = userRo.SYZT,
                    SFSC = 0,
                    Creater = this.CurrentLoginUser.ID,
                    CreateTime = DateTime.Now
                };

                var userAddModel = await UsersServices.DbContext.InsertAsync(userModel);

                if (userAddModel != null && userAddModel.ID > 0)
                {
                    await userRoleService.AddUserRole(userAddModel.ID, userRo.Roles);

                    return BulResult.SuccessNonData();
                }
                else
                    return BulResult.FailNonData(-10, "添加用户失败");
            }
        }

    }
}
