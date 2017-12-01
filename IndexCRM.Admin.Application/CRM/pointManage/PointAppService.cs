using System;
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
                pr.Id = Guid.NewGuid().ToString().ToUpper();
                pr.VipId = input.VipId;
                pr.PointChange = input.Amount;
                pr.PointExplain = input.Explain;
                pr.PosNo = "";
                pr.AddMan = AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync((long)AbpSession.UserId)).Name;
                pr.AddTime = DateTime.Now;
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

        public async Task SendPoint(ChangePointInput input)
        {
            if (input.Amount <= 0)
            {
                throw new UserFriendlyException("修改数量不能小于等于零");
            }

            var vip = _vipRepository.FirstOrDefault(u => u.VipCode == input.VipId);
            if (vip != null)
            {
                if (vip.Status == "2")
                {
                    throw new UserFriendlyException("该会员已冻结");
                }
                else
                {
                    var vipPoint = _pointRepository.FirstOrDefault(a => a.VipId == vip.Id);
                    vipPoint.VipPoint = vipPoint.VipPoint + input.Amount;
                    _pointRepository.Update(vipPoint);

                    PointRecord pr = new PointRecord();
                    pr.Id = Guid.NewGuid().ToString().ToUpper();
                    pr.VipId = vip.Id;
                    pr.PointChange = input.Amount;
                    pr.PointExplain = input.Explain;
                    pr.PosNo = "";
                    pr.AddMan = AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync((long)AbpSession.UserId)).Name;
                    pr.AddTime = DateTime.Now;
                    _pointRecordRepository.Insert(pr);
                }
            }
            else
            {
                throw new UserFriendlyException("该会员不存在");
            }



        }

        public async Task<PagedResultDto<VipPointRecordListDto>> GetSendPointRecordList(GetVipPointRecordInput input)
        {
            var addMan = AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync((long)AbpSession.UserId)).Name;
            var vipPointRecord = _pointRecordRepository.GetAll()
                .Where(u => u.AddMan == addMan && u.AddTime >= DbFunctions.TruncateTime(DateTime.Now));

            var query = from v in vipPointRecord
                        join c in _vipRepository.GetAll() on v.VipId equals c.Id
                        select new
                        {
                            v,
                            c = c == null ? null : new { c.Id, c.VipCode }
                        };

            var vipPointRecordCount = await query.CountAsync();
            var vipPointRecordList = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            var vipPointRecordListDto = vipPointRecordList.Select(s => s.v).MapTo<List<VipPointRecordListDto>>();

            vipPointRecordListDto = vipPointRecordListDto.Select(v =>
            {
                var dto = v;
                dto.VipCode =
                    vipPointRecordList.Select(s => s.c)
                        .Where(w => w.Id == v.VipId)
                        .Select(si => si.VipCode)
                        .FirstOrDefault();
                return dto;
            }).ToList();

            return new PagedResultDto<VipPointRecordListDto>(
                vipPointRecordCount,
                vipPointRecordListDto
                );
        }
    }
}
