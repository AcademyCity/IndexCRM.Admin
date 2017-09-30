using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using IndexCRM.Admin.Authorization;
using IndexCRM.Admin.CRM.pointManage.Dto;
using Abp.Threading;

namespace IndexCRM.Admin.CRM.pointManage
{
    [AbpAuthorize(AppPermissions.CRM_VipManage)]
    public class PointAppService : AdminAppServiceBase, IPointAppService
    {

        private readonly IRepository<Vip, string> _vipRepository;
        private readonly IRepository<Point, string> _pointRepository;
        private readonly IRepository<PointRecord, string> _pointRecordRepository;


        public PointAppService(
            IRepository<Vip, string> vipRepository,
            IRepository<Point, string> pointRepository,
            IRepository<PointRecord, string> pointRecordRepository)
        {
            _vipRepository = vipRepository;
            _pointRepository = pointRepository;
            _pointRecordRepository = pointRecordRepository;
        }

        public async Task ChangePoint(ChangePointInput input)
        {
            if (input.Amount != 0)
            {
                var vipPoint = _pointRepository.FirstOrDefault(a => a.VipId == input.VipId);
                vipPoint.VipPoint = vipPoint.VipPoint + input.Amount;
                _pointRepository.Update(vipPoint);

                PointRecord pr = new PointRecord();
                pr.Id = System.Guid.NewGuid().ToString().ToUpper();
                pr.VipId = input.VipId;
                pr.PointChange = input.Amount;
                pr.PointExplain = input.Explain;
                pr.PosNo = "";
                pr.AddMan = AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync((long)AbpSession.UserId)).Name;
                pr.AddTime = System.DateTime.Now;
                _pointRecordRepository.Insert(pr);
            }
            else
            {
                throw new UserFriendlyException("修改数量不能等于零");
            }

        }

        [AbpAuthorize(AppPermissions.CRM_VipManage)]
        public async Task<PagedResultDto<VipPointRecordListDto>> GetVipPointRecordList(GetVipPointRecordInput input)
        {
            var vipPointRecord = _pointRecordRepository.GetAll()
                .Where(u => u.VipId == input.VipId);

            var vipPointRecordCount = await vipPointRecord.CountAsync();
            var vipPointRecordList = await vipPointRecord
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            var vipPointRecordListDto = vipPointRecordList.MapTo<List<VipPointRecordListDto>>();

            return new PagedResultDto<VipPointRecordListDto>(
                vipPointRecordCount,
                vipPointRecordListDto
                );
        }


    }
}
