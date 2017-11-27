using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using IndexCRM.Admin.CRM.couponManage.Dto;

namespace IndexCRM.Admin.CRM.couponManage
{
    public interface ICouponAppService : IApplicationService
    {
        Task SendCoupon(SendCouponInput input);

        Task<List<CouponConfigListDto>> GetCouponConfigList();

        Task<PagedResultDto<VipCouponListDto>> GetVipCouponList(GetVipCouponInput input);

        Task<GetCouponConfigForEditOutput> GetCouponConfigForEdit(GetCouponConfigInput input);
    }
}