using Abp.AutoMapper;
using System;

namespace IndexCRM.Admin.CRM.couponManage.Dto
{
    [AutoMapFrom(typeof(CouponConfig))]
    public class GetCouponConfigForEditInput
    {
        public GetCouponConfigForEditDto CouponConfig { get; set; }

    }
}