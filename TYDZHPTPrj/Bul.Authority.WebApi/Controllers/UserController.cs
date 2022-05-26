using Bul.Authority.Application;
using Bul.Authority.Application.DataTranslateObject;
using Bul.Authority.Application.RequestObject;
using Bul.Authority.Entity;
using Bul.Authority.Service;
using Bul.System.Common;
using Bul.System.Extension.NetCore;
using Bul.System.Result;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bul.Authority.WebApi.Controllers
{
    public class UserController : AuthorityBaseController
    {
        private readonly SqUserApplication sqUserApplication;
        public UserController(SqUserApplication userApplication)
        {
            this.sqUserApplication = userApplication;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public AbstractResult UserLogin(LoginUserRo loginUser)
        {
            try
            {
                var loginResult = this.sqUserApplication.ValidateUserLogin(loginUser);
                if (loginResult.Code != 0)
                    return loginResult;

                var sqUserRoleApplication = HttpContext.GetService<SqUserRoleApplication>();

                var userRoles = sqUserRoleApplication.GetUserRoleByUserId(loginResult.Data.ID, loginResult.Data.SSGSID);

                var jwtBearer = this.HttpContext.GetService<JwtBearer>();
                var token = jwtBearer.LoginToken(new
                {
                    loginResult.Data.ID,
                    loginResult.Data.YHM,
                    loginResult.Data.SJH,
                    loginResult.Data.SSGSID,
                    loginResult.Data.SSYGID,
                    loginResult.Data.XM,
                    loginResult.Data.SFGLY,
                    SqUserRoles = userRoles?.Data
                }, 8);

                BulLogger.Info("登录成功");

                return BulResult<string>.Success(token);
            }
            catch (Exception er)
            {
                return BulResult.FailNonData(-10, er.Message);
            }
        }

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="saveUserRo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("save")]
        public async Task<AbstractResult> SaveUser(SaveUserRo saveUserRo)
        {
            var result = await sqUserApplication.SaveUser(saveUserRo);

            return result;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="ro"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("list")]
        public async Task<BulResult<Page<IEnumerable<SqUsersDto>>>> GetSqUsersList(UserListRo ro)
        {
            if (ro == null)
                return BulResult<IEnumerable<SqUsersDto>>.PageFail(-1, "参数错误");

            var userService = HttpContext.GetService<SqUsersService>();
            var userQuery = userService.DbContext.Query<SqUsers>();

            if (!string.IsNullOrEmpty(ro.YHM))
                userQuery = userQuery.Where(w => w.YHM.Contains(ro.YHM));

            if (!string.IsNullOrEmpty(ro.XM))
                userQuery = userQuery.Where(w => w.XM.Contains(ro.XM));

            if (!string.IsNullOrEmpty(ro.SJH))
                userQuery = userQuery.Where(w => w.SJH.Contains(ro.SJH));

            if (this.CurrentUser.SSGSID != 0)
            {
                userQuery = userQuery.Where(w => w.SSGSID == this.CurrentUser.SSGSID);
            }
            else
            {
                if (ro.SSGSID != null && ro.SSGSID.Value > 0)
                    userQuery = userQuery.Where(w => w.SSGSID == ro.SSGSID.Value);
            }

            if (ro.SYZT != null)
                userQuery = userQuery.Where(w => w.SYZT == ro.SYZT.Value);

            var userList = await userQuery.TakePage(ro.PageIndex, ro.PageSize).ToListAsync();

            var totalCount = await userQuery.CountAsync();

            var sqUserDto = userList.Adapt<List<SqUsersDto>>();

            if (sqUserDto != null && sqUserDto.Count > 0)
            {
                var userIds = sqUserDto.Select(s => s.ID);

                var userRoleService = HttpContext.GetService<SqUserRoleService>();
                var roleService = HttpContext.GetService<SqRoleServices>();

                var roleQuery = roleService.DbContext.Query<SqRoles>().LeftJoin<SqUserRole>((role, userRole) => role.ID == userRole.RoleID);

                var roleList = await roleQuery.Select((role, userRole) => new
                {
                    Role = role,
                    UserRole = userRole
                }).Where(a => a.Role.SFSC == 0 && a.Role.SYZT == 1 && userIds.Contains(a.UserRole.UserID)).Select(a => new SqRoleUserIdBo
                {
                    ID = a.Role.ID,
                    JSBM = a.Role.JSBM,
                    JSMC = a.Role.JSMC,
                    UserID = a.UserRole.UserID,
                }).ToListAsync();

                foreach (var item in sqUserDto)
                {
                    if (item.SYZT == 1)
                        item.SYZTName = "启用";
                    else if (item.SYZT == 0)
                        item.SYZTName = "禁用";


                    if (item.SFSC == 1)
                        item.SFSCName = "已删除";
                    else if (item.SFSC == 0)
                        item.SFSCName = "未删除";

                    var jsmcs = roleList.Where(w => w.UserID == item.ID)?.Select(w => w.JSMC);
                    if (jsmcs != null)
                        item.RoleNames = string.Join(",", jsmcs);

                    item.SSYGXM = item.XM;

                    if (item.SSGSID == 0)
                        item.SSGSName = "本公司";
                    else
                        item.SSGSName = "-";
                }
            }

            return BulResult<IEnumerable<SqUsersDto>>.PageSuccess(sqUserDto, ro.PageIndex, ro.PageSize, totalCount);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="ro"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("model")]
        public async Task<BulResult<SqUserRolesDto>> GetUser(EntityIdRo ro)
        {
            if (ro == null || ro.Id <= 0)
                return BulResult<SqUserRolesDto>.Fail(-1, "参数错误");

            var userService = HttpContext.GetService<SqUsersService>();

            var query = userService.DbContext.Query<SqUsers>().Where(w => w.ID == ro.Id);

            if (this.CurrentUser.SSGSID != 0)
                query = query.Where(w => w.SSGSID == this.CurrentUser.SSGSID);

            var model = await query.FirstOrDefaultAsync();

            if (model == null)
                return BulResult<SqUserRolesDto>.Fail(-2, "未检索到用户信息");

            var userRoleService = HttpContext.GetService<SqUserRoleService>();

            var userRoleQuery = userRoleService.DbContext.Query<SqUserRole>().LeftJoin<SqRoles>((ur, rl) => ur.RoleID == rl.ID);

            userRoleQuery = userRoleQuery.Where((ur, rl) => ur.SSGSID == this.CurrentUser.SSGSID && ur.UserID == model.ID);

            var userRoleList = await userRoleQuery.Select((ur, rl) => rl).ToListAsync();

            if (userRoleList == null || userRoleList.Count <= 0)
                return BulResult<SqUserRolesDto>.Fail(-3, "未检索到用户角色信息");

            var userRoleDto = model.Adapt<SqUserRolesDto>();

            userRoleDto.Roles = userRoleList;

            return BulResult<SqUserRolesDto>.Success(userRoleDto);
        }

        /// <summary>
        /// 删除用户（逻辑删除）
        /// </summary>
        /// <param name="ro"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete")]
        public async Task<AbstractResult> DelUser(EntityIdRo ro)
        {
            if (ro == null || ro.Id <= 0)
                return BulResult.FailNonData(-1, "参数错误");

            var userService = HttpContext.GetService<SqUsersService>();

            var userModelQuery = userService.DbContext.Query<SqUsers>();

            userModelQuery = userModelQuery.Where(w => w.ID == ro.Id);

            var userModel = await userModelQuery.FirstOrDefaultAsync();

            if (userModel == null)
                return BulResult.FailNonData(-2, "未检索到当前账号");

            if (this.CurrentUser.SSGSID != userModel.SSGSID)
                return BulResult.FailNonData(-3, "您没有删除此用户的权限");

            userModel.SFSC = 1;
            userModel.Updater = this.CurrentUser.ID;
            userModel.UpdateTime = DateTime.Now;

            var isUpdateSuc = await userService.DbContext.UpdateAsync(userModel);

            if (isUpdateSuc > 0)
                return BulResult.SuccessNonData();

            return BulResult.FailNonData(-3, "执行删除失败");
        }

        /// <summary>
        /// 启用禁用用户
        /// </summary>
        /// <param name="userRo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("disablen")]
        public async Task<AbstractResult> SetEnableAndDis(DisEnRoleRo userRo)
        {
            if (userRo == null || userRo.Id <= 0)
                return BulResult.FailNonData(-1, "参数错误");

            if (userRo.RoleStatue != 1 && userRo.RoleStatue != 0)
                return BulResult.FailNonData(-1, "参数错误");

            var userService = HttpContext.GetService<SqUsersService>();

            var userQuery = userService.DbContext.Query<SqUsers>().Where(w => w.ID == userRo.Id);

            var userModel = await userQuery.FirstOrDefaultAsync();

            if (userModel == null)
                return BulResult.FailNonData(-1, "参数错误");

            if (this.CurrentUser.SSGSID != 0)
            {
                if (this.CurrentUser.SSGSID != userModel.SSGSID)
                    return BulResult.FailNonData(-2, "您没有权限修改禁用启用此用户");
            }

            userModel.SYZT = userRo.RoleStatue;
            userModel.Updater = this.CurrentUser.ID;
            userModel.UpdateTime = DateTime.Now;

            var isUdpdSuc = await userService.DbContext.UpdateAsync(userModel);

            if (isUdpdSuc > 0)
                return BulResult.SuccessNonData();

            return BulResult.FailNonData(-3, "启用禁用失败");
        }

    }
}
