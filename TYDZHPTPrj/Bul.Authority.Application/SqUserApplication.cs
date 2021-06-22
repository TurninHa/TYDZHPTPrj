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
    public class SqUserApplication
    {
        private readonly SqUsersServices UsersServices;
        public SqUserApplication(SqUsersServices services)
        {
            this.UsersServices = services;
        }

        public BulResult<SqUsers> GetUserModelByYhmAndSjh(string yhm, string sjh)
        {
            if (string.IsNullOrEmpty(yhm))
                return BulResult<SqUsers>.Fail(-1, "用户名不能为空");

            var query = this.UsersServices.Db.Query<SqUsers>();
            query = query.Where(w => w.YHM == yhm && w.SJH == sjh);


            var model = query?.FirstOrDefault();
            if (model == null)
                return BulResult<SqUsers>.Fail(-2, "未检索到数据");

            return BulResult<SqUsers>.Success(model);
        }
    }
}
