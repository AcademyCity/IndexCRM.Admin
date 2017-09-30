using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using IndexCRM.Admin.Authorization;
using IndexCRM.Admin.CRM.couponManage.Dto;

namespace IndexCRM.Admin.CRM.couponManage
{
    [AbpAuthorize(AppPermissions.CRM_VipManage)]
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
            //if (input.Amount != 0)
            //{
            //    var vipPoint = _pointRepository.FirstOrDefault(a => a.VipId == input.VipId);
            //    vipPoint.VipPoint = vipPoint.VipPoint + input.Amount;
            //    _pointRepository.Update(vipPoint);

            //    PointRecord pr = new PointRecord();
            //    pr.Id = System.Guid.NewGuid().ToString().ToUpper();
            //    pr.VipId = input.VipId;
            //    pr.PointChange = input.Amount;
            //    pr.PointExplain = input.Explain;
            //    pr.PosNo = "";
            //    pr.AddMan = AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync((long)AbpSession.UserId)).Name;
            //    pr.AddTime = System.DateTime.Now;
            //    _pointRecordRepository.Insert(pr);
            //}
            //else
            //{
            //    throw new UserFriendlyException("修改数量不能等于零");
            //}
        }

        public async Task<List<CouponConfigListDto>> GetCouponConfigList()
        {
            var couponConfigList = _couponConfigRepository.GetAll()
                    .Where(u => u.IsDelete == false)
                    .OrderByDescending(a=>a.AddTime).ToList();

            var couponConfigListDto = couponConfigList.MapTo<List<CouponConfigListDto>>();

            return couponConfigListDto;
        }
    }
}
