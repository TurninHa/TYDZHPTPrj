using Bul.Authority.Application.ApplicationBase;
using Bul.Authority.Entity;
using Bul.Authority.Service;
using Bul.System.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Application
{
    public class SqUserRoleApplication : BulAuthorityApplication
    {
        private readonly SqUserRoleService _SqUserRoleService;
        public SqUserRoleApplication(SqUserRoleService userRoleService)
        {
            _SqUserRoleService = userRoleService;
        }

        public BulResult<IEnumerable<SqUserRole>> GetUserRoleByUserId(long userId, long ssgsId)
        {
            if (userId <= 0 || ssgsId < 0)
                return BulResult<IEnumerable<SqUserRole>>.Fail(-1, "请传入用户id或公司Id");

            var query = this._SqUserRoleService.DbContext.Query<SqUserRole>();

            query = query.Where(w => w.UserID == userId && w.SSGSID == ssgsId);

            var result = query.ToList();

            return BulResult<IEnumerable<SqUserRole>>.Success(result);
        }
    }
}
