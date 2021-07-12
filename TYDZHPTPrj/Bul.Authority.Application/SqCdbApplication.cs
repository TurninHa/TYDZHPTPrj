using Bul.Authority.Application.DataTranslateObject;
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
    public class SqCdbApplication
    {
        private readonly SqCdbService sqCdbService;
        public SqCdbApplication(SqCdbService cdbService)
        {
            sqCdbService = cdbService;
        }

        public async Task<BulResult<SqCdb>> SaveSqCd(SqCdb sqCdb)
        {
            var result = await this.sqCdbService.Save(sqCdb);

            return result;
        }

        /// <summary>
        /// 获取子节点菜单列表
        /// </summary>
        /// <param name="fcdid"></param>
        /// <returns></returns>
        public async Task<BulResult<IEnumerable<SqCdb>>> GetChildMenuList(long fcdid)
        {
            if (fcdid < 0)
                return BulResult<IEnumerable<SqCdb>>.Fail(-1, "父菜单ID不能小于0");

            var query = this.sqCdbService.Db.Query<SqCdb>();
            query = query.Where(w => w.FCDID == fcdid);

            var result = await query.ToListAsync();

            return BulResult<IEnumerable<SqCdb>>.Success(result);
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        public async Task<BulResult<IEnumerable<SqCdbDto>>> GetSqCdbTree()
        {
            var sqCdList = await this.sqCdbService.Db.Query<SqCdb>().ToListAsync();

            if (sqCdList == null || sqCdList.Count == 0)
                return BulResult<IEnumerable<SqCdbDto>>.Fail(-1, "未查询到菜单列表");


        }
    }
}
