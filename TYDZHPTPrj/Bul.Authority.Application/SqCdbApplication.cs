using Bul.Authority.Application.DataTranslateObject;
using Bul.Authority.Application.Enumer;
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
        /// 管理时获取菜单树
        /// </summary>
        /// <returns></returns>
        public BulResult<IEnumerable<SqCdbDto>> GetSqCdbTree()
        {
            return this.GetSqCdbTree(0);
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        public BulResult<IEnumerable<SqCdbDto>> GetSqCdbTree(long ssgsId)
        {
            var sqcdbQuery = this.sqCdbService.Db.Query<SqCdb>();
            sqcdbQuery = sqcdbQuery.Where(w => w.SYZT == (int)SYZTType.Enable);

            if (ssgsId > 0)
                sqcdbQuery = sqcdbQuery.Where(w => w.SSGSID == ssgsId);

            var sqCdList = sqcdbQuery.ToList();

            if (sqCdList == null || sqCdList.Count == 0)
                return BulResult<IEnumerable<SqCdbDto>>.Fail(-1, "未查询到菜单列表");

            var trees = new List<SqCdbDto>();

            var firstLevsCds = sqCdList.Where(w => w.FCDID == 0);

            foreach (var item in firstLevsCds)
            {
                var root = new SqCdbDto
                {
                    Title = item.CDMC,
                    Key = item.ID + "_" + item.CDBM,
                    SqCdbDtos = new List<SqCdbDto>()
                };

                trees.Add(root);

                RecursionCd(sqCdList, item.ID, root);
            }

            return BulResult<IEnumerable<SqCdbDto>>.Success(trees);
        }

        private void RecursionCd(IList<SqCdb> source, long parentId, SqCdbDto parentNode)
        {
            if (parentNode == null) return;

            var childNodes = source.Where(w => w.FCDID == parentId);
            if (childNodes == null || !childNodes.Any()) return;

            foreach (var item in childNodes)
            {
                var sqcdbTree = new SqCdbDto()
                {
                    Title = item.CDMC,
                    Key = item.ID + "_" + item.CDBM,
                    SqCdbDtos = new List<SqCdbDto>()
                };

                parentNode.SqCdbDtos.Add(sqcdbTree);

                RecursionCd(source, item.ID, sqcdbTree);
            }
        }
    }
}
