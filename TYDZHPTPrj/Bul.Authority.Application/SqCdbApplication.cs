using Bul.Authority.Application.ApplicationBase;
using Bul.Authority.Application.DataTranslateObject;
using Bul.Authority.Application.Enumer;
using Bul.Authority.Entity;
using Bul.Authority.Service;
using Bul.System.Result;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Application
{
    public class SqCdbApplication : BulAuthorityApplication
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

        /// <summary>
        /// 根据条件获取列表
        /// </summary>
        /// <param name="sqCdbListDto"></param>
        /// <returns></returns>
        public BulResult<IEnumerable<SqCdbListDto>> GetListByWhere(SqCdbListDto sqCdbListDto)
        {
            if (sqCdbListDto == null)
                return BulResult<IEnumerable<SqCdbListDto>>.Fail(-1, "参数错误");

            var query = this.sqCdbService.Db.Query<SqCdb>();
            query = query.Where(w => w.SYZT == 1);

            if (!string.IsNullOrEmpty(sqCdbListDto.CDBM))
                query = query.Where(w => w.CDBM.Contains(sqCdbListDto.CDBM));

            if (!string.IsNullOrEmpty(sqCdbListDto.CDMC))
                query = query.Where(w => w.CDMC.Contains(sqCdbListDto.CDMC));

            var result = query.ToList();
            var resultDto = result.Adapt<IEnumerable<SqCdbListDto>>();

            return BulResult<IEnumerable<SqCdbListDto>>.Success(resultDto);
        }

        public BulResult<Page<IEnumerable<SqCdbListDto>>> GetPageListByWhere(SqCdbListDto condition, int pageIndex, int pageSize)
        {
            if (condition == null)
                return BulResult<IEnumerable<SqCdbListDto>>.PageFail(-1, "参数错误");

            var query = this.sqCdbService.Db.Query<SqCdb>();
            query = query.Where(w => w.SYZT == 1);

            if (!string.IsNullOrEmpty(condition.CDBM))
                query = query.Where(w => w.CDBM.Contains(condition.CDBM));

            if (!string.IsNullOrEmpty(condition.CDMC))
                query = query.Where(w => w.CDMC.Contains(condition.CDMC));

            var pageCount = query.Count();

            var result = query.TakePage(pageIndex, pageSize).ToList();

            var resultDto = result.Adapt<IEnumerable<SqCdbListDto>>();

            return BulResult<IEnumerable<SqCdbListDto>>.PageSuccess(resultDto, pageIndex, pageSize, pageCount);
        }
    }
}
