using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.Runtime.Validation;
using IndexCRM.Admin.Dto;

namespace IndexCRM.Admin.CRM.couponManage.Dto
{
    public class SendCouponInput
    {
        [DisplayName("会员Id")]
        public string VipId { get; set; }

        [DisplayName("优惠券Id")]
        public string CouponConfigId { get; set; }

        [DisplayName("修改原因")]
        public string Explain { get; set; }

    }
}