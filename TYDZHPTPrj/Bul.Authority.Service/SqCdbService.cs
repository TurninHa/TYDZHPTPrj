using Bul.Authority.DBConnection.DbContextBaseService;
using Bul.Authority.Entity;
using Bul.System.Result;
using Chloe;
using Chloe.Extensions;
using Chloe.MySql;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Service
{
    public class SqCdbService : BaseService
    {

        public SqCdbService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        { }

        /// <summary>
        /// 保存菜单
        /// </summary>
        /// <param name="cdb"></param>
        /// <returns></returns>
        public async Task<BulResult<SqCdb>> Save(SqCdb cdb)
        {
            if (cdb == null)
                return BulResult<SqCdb>.Fail(-1, "参数错误");

            if (string.IsNullOrEmpty(cdb.CDBM))
                return BulResult<SqCdb>.Fail(-2, "菜单编码不能为空");

            if (string.IsNullOrEmpty(cdb.CDMC))
                return BulResult<SqCdb>.Fail(-3, "菜单名称不能为空");

            if (cdb.FCDID == null)
                return BulResult<SqCdb>.Fail(-3, "父级菜单Id不能为空");

            //Expression<Func<SqCdb, bool>> where = w => (w.CDBM == cdb.CDBM || w.CDMC == cdb.CDMC);

            //if (cdb.ID > 0)
            //{
            //    Expression<Func<SqCdb, bool>> wherAnd = a => a.ID != cdb.ID;
            //    where = (Expression<Func<SqCdb, bool>>)where.And(wherAnd);
            //}

            var isExistMenu = this.Db.Query<SqCdb>();
            isExistMenu = isExistMenu.Where(w => (w.CDBM == cdb.CDBM || w.CDMC == cdb.CDMC));

            if (cdb.ID > 0)
                isExistMenu = isExistMenu.Where(w => w.ID != cdb.ID);


            if (isExistMenu != null && isExistMenu.Any())
                return BulResult<SqCdb>.Fail(-5, "菜单编码或菜单名称已存在");

            if (cdb.ID > 0)
            {
                var result = await this.Db.UpdateAsync<SqCdb>(cdb);

                if (result > 0)
                    return BulResult<SqCdb>.Success(cdb, result.ToString());
                else
                    return BulResult<SqCdb>.Fail(-4, "修改失败");
            }
            else
            {
                var result = await this.Db.InsertAsync<SqCdb>(cdb);

                if (result != null)
                    return BulResult<SqCdb>.Success(cdb);
                else
                    return BulResult<SqCdb>.Fail(-6, "保存失败");
            }
        }

    }
}
