using Bul.Authority.DBConnection.DbContextBaseService;
using Bul.Entity.Interface;
using Bul.System.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Db.Extension
{
    public static class BaseServiceExtension
    {
        public static BulResult<Page<IEnumerable<IBulEntity>>> GetPageListByWhere(this BaseService baseService, Expression<Func<IBulEntity, bool>> expression, int pageIndex, int pageSize)
        {
            var query = baseService.DbContext.Query<IBulEntity>().Where(expression);

            var count = query.Count();

            var pageList = query.TakePage(pageIndex, pageSize).ToList();

            return BulResult<IEnumerable<IBulEntity>>.PageSuccess(pageList, pageIndex, pageSize, count);
        }
    }
}
