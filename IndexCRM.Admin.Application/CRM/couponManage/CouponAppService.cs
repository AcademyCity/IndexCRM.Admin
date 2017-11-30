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
using Abp.Extensions;
using System.Configuration;

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
                .Where(u => u.VipId == input.VipId);

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

            foreach (var item in vipCouponListDto)
            {
                if (item.EndTime >= DateTime.Now)
                {
                    item.IsValidity = true;
                }
                else
                {
                    item.IsValidity = false;
                }
            }

            return new PagedResultDto<VipCouponListDto>(
                vipCouponCount,
                vipCouponListDto
                );
        }

        public async Task<GetCouponConfigForEditDto> GetCouponConfigForEdit(GetCouponConfigInput input)
        {
            if (string.IsNullOrEmpty(input.CouponConfigId))
            {
                //Creating a new coupon
                var couponConfigDto = new GetCouponConfigForEditDto();

                return couponConfigDto;
            }
            else
            {
                //Editing an existing coupon
                var couponConfig = _couponConfigRepository.FirstOrDefault(u => u.Id == input.CouponConfigId);

                var couponConfigDto = couponConfig.MapTo<GetCouponConfigForEditDto>();

                couponConfigDto.CouponImg = ConfigurationManager.AppSettings["ImgSiteAddress"] + couponConfigDto.CouponImg;

                return couponConfigDto;
            }
        }

        public async Task CreateOrUpdateCoupon(GetCouponConfigForEditInput input)
        {
            if (string.IsNullOrEmpty(input.CouponConfig.Id))
            {
                await CreateCouponAsync(input);
            }
            else
            {
                await UpdateCouponAsync(input);
            }
        }

        protected virtual async Task UpdateCouponAsync(GetCouponConfigForEditInput input)
        {
            var couponConfig = await _couponConfigRepository.FirstOrDefaultAsync(u => u.Id == input.CouponConfig.Id);

            //Update user properties
            input.CouponConfig.MapTo(couponConfig); //Passwords is not mapped (see mapping configuration)
            couponConfig.ModifyMan = AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync((long)AbpSession.UserId)).Name;
            couponConfig.ModifyTime = DateTime.Now;

            await _couponConfigRepository.UpdateAsync(couponConfig);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        protected virtual async Task CreateCouponAsync(GetCouponConfigForEditInput input)
        {
            var couponConfig = input.CouponConfig.MapTo<CouponConfig>();

            couponConfig.Id = Guid.NewGuid().ToString().ToUpper();
            couponConfig.IsDelete = false;
            couponConfig.AddMan = AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync((long)AbpSession.UserId)).Name;
            couponConfig.AddTime = DateTime.Now;
            couponConfig.ModifyMan = AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync((long)AbpSession.UserId)).Name;
            couponConfig.ModifyTime = DateTime.Now;

            await _couponConfigRepository.InsertAsync(couponConfig);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task<PagedResultDto<GetCouponConfigForEditDto>> GetCouponConfigList(GetCouponConfigInput input)
        {
            var couponConfig = _couponConfigRepository.GetAll()
                .Where(u => u.IsDelete == false)
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), u => u.CouponName == input.Filter);

            var couponConfigCount = await couponConfig.CountAsync();
            var couponConfigList = await couponConfig
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();


            var couponConfigListDto = couponConfigList.MapTo<List<GetCouponConfigForEditDto>>();

            foreach (var item in couponConfigListDto)
            {
                item.SendCouponNum = _couponRepository.Count(a => a.CouponConfigId == item.Id);
            }

            return new PagedResultDto<GetCouponConfigForEditDto>(
                couponConfigCount,
                couponConfigListDto
                );
        }

        public async Task DeleteCouponConfig(GetCouponConfigInput input)
        {
            //Editing an existing coupon
            var couponConfig = _couponConfigRepository.FirstOrDefault(u => u.Id == input.CouponConfigId);
            couponConfig.IsDelete = true;
            await _couponConfigRepository.UpdateAsync(couponConfig);

        }

        public async Task<string> GetCheckCouponName(GetVipCouponInput input)
        {
            var vipCoupon = _couponRepository.FirstOrDefault(u => u.CouponCode == input.CouponCode);
            if (vipCoupon != null)
            {
                if (vipCoupon.IsUse)
                {
                    throw new UserFriendlyException("优惠券已核销");
                }
                else
                {
                    if (vipCoupon.EndTime<=DateTime.Now)
                    {
                        throw new UserFriendlyException("优惠券已过期");
                    }
                    else
                    {
                        return _couponConfigRepository.FirstOrDefault(u => u.Id == vipCoupon.CouponConfigId).CouponName;
                    } 
                }
            }
            else
            {
                throw new UserFriendlyException("优惠券不存在");
            }
        }

        public async Task CheckCoupon(GetVipCouponInput input)
        {
            //Editing an existing coupon
            var vipCoupon = _couponRepository.FirstOrDefault(u => u.CouponCode == input.CouponCode);
            vipCoupon.IsUse = true;
            vipCoupon.ModifyMan = AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync((long)AbpSession.UserId)).Name;
            vipCoupon.ModifyTime = DateTime.Now;
            await _couponRepository.UpdateAsync(vipCoupon);

        }

        public async Task<PagedResultDto<VipCouponListDto>> GetCheckCouponList(GetVipCouponInput input)
        {
            var modifyMan = AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync((long)AbpSession.UserId)).Name;
            var vipCoupon = _couponRepository.GetAll()
                .Where(u => u.IsUse  && u.ModifyMan == modifyMan && u.ModifyTime.Value >= DbFunctions.TruncateTime(DateTime.Now));

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
    }
}
