using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Threading;
using Abp.UI;
using IndexCRM.Admin.Authorization;
using IndexCRM.Admin.CRM.couponManage.Dto;
using IndexCRM.Admin.Function;
using System.Linq.Dynamic;
using Abp.Linq.Extensions;

namespace IndexCRM.Admin.CRM.couponManage
{
    [AbpAuthorize(AppPermissions.CRM_CouponManage)]
    public class CouponAppService : AdminAppServiceBase, ICouponAppService
    {

        private readonly IRepository<Vip, string> _vipRepository;
        private readonly IRepository<Coupon, string> _couponRepository;
        private readonly IRepository<CouponConfig, string> _couponConfigRepository;


        public CouponAppService(
            IRepository<Vip, string> vipRepository,
            IRepository<Coupon, string> couponRepository,
            IRepository<CouponConfig, string> couponConfigRepository)
        {
            _vipRepository = vipRepository;
            _couponRepository = couponRepository;
            _couponConfigRepository = couponConfigRepository;
        }

        public async Task SendCoupon(SendCouponInput input)
        {
            if (!string.IsNullOrEmpty(input.CouponConfigId))
            {
                var couponConfig = _couponConfigRepository.FirstOrDefault(a => a.Id == input.CouponConfigId);
                if (couponConfig != null)
                {
                    Coupon c = new Coupon();
                    c.Id = Guid.NewGuid().ToString().ToUpper();
                    c.CouponConfigId = input.CouponConfigId;
                    c.VipId = input.VipId;
                    c.CouponCode = Utils.GetCodeNo();
                    c.IsUse = false;
                    c.AddExplain = input.Explain;
                    c.AddMan = AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync((long)AbpSession.UserId)).Name;
                    c.AddTime = DateTime.Now;
                    c.ModifyMan = AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync((long)AbpSession.UserId)).Name;
                    c.ModifyTime = DateTime.Now;

                    if (couponConfig.ValidityMode == "1")
                    {
                        c.StartTime = couponConfig.StartTime;
                        c.EndTime = couponConfig.EndTime;
                    }

                    if (couponConfig.ValidityMode == "2")
                    {
                        c.StartTime = System.DateTime.Now.AddDays(Convert.ToDouble(couponConfig.EffectDate));
                        c.EndTime = System.DateTime.Now.AddDays(Convert.ToDouble(couponConfig.ValidDate));
                    }

                    _couponRepository.Insert(c);
                }
                else
                {
                    throw new UserFriendlyException("优惠券不存在");
                }

            }
            else
            {
                throw new UserFriendlyException("优惠券Id为空");
            }
        }

        public async Task<List<CouponConfigListDto>> GetCouponConfigList()
        {
            var couponConfigList = _couponConfigRepository.GetAll()
                    .Where(u => u.IsDelete == false)
                    .OrderByDescending(a => a.AddTime).ToList();

            var couponConfigListDto = couponConfigList.MapTo<List<CouponConfigListDto>>();

            return couponConfigListDto;
        }

        public async Task<PagedResultDto<VipCouponListDto>> GetVipCouponList(GetVipCouponInput input)
        {
            var vipCoupon = _couponRepository.GetAll()
                .Where(u => u.VipId == input.VipId&&u.EndTime>DateTime.Now);

            var query = from v in vipCoupon
                        join c in _couponConfigRepository.GetAll() on v.CouponConfigId equals c.Id
                        select new
                        {
                            v,
                            c = c == null ? null : new { c.Id, c.CouponName }
                        };

            var vipCouponCount = await query.CountAsync();
            var vipCouponList = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            var vipCouponListDto = vipCouponList.Select(s => s.v).MapTo<List<VipCouponListDto>>();

            vipCouponListDto = vipCouponListDto.Select(v =>
            {
                var dto = v;
                dto.CouponName =
                    vipCouponList.Select(s => s.c)
                        .Where(w => w.Id == v.CouponConfigId)
                        .Select(si => si.CouponName)
                        .FirstOrDefault();
                return dto;
            }).ToList();

            return new PagedResultDto<VipCouponListDto>(
                vipCouponCount,
                vipCouponListDto
                );
        }

        public async Task<GetCouponConfigForEditOutput> GetCouponConfigForEdit(GetCouponConfigInput input)
        {
            if (string.IsNullOrEmpty(input.CouponConfigId))
            {
                //Creating a new coupon
                var couponConfigDto = new GetCouponConfigForEditOutput();

                return couponConfigDto;
            }
            else
            {
                //Editing an existing coupon
                var couponConfig = _couponConfigRepository.FirstOrDefault(u => u.Id==input.CouponConfigId);

                var couponConfigDto = couponConfig.MapTo<GetCouponConfigForEditOutput>();

                return couponConfigDto;
            }

           
        }
        
    }
}
