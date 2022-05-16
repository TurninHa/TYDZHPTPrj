using Bul.Authority.DBConnection.DbContextBaseService;
using Bul.Authority.Entity;
using Bul.Authority.Service.AuthorityServiceBase;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Service
{
    public class SqUserRoleService : AuthorityBaseService
    {
        public async Task AddUserRole(long userId, IEnumerable<long> RoleIds)
        {
            var userRoleModels = new List<SqUserRole>();
            foreach (var item in RoleIds)
            {
                userRoleModels.Add(new SqUserRole
                {
                    UserID = userId,
                    RoleID = item,
                    SSGSID = this.CurrentLoginUser.SSGSID,
                    Creater = this.CurrentLoginUser.ID,
                    CreateTime = DateTime.Now,
                });
            }

            await DbContext.InsertRangeAsync(userRoleModels);
        }
    }
}
