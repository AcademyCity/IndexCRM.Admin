using Abp.Runtime.Validation;
using IndexCRM.Admin.Dto;

namespace IndexCRM.Admin.CRM.couponManage.Dto
{
    public class GetVipCouponInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string VipId { get; set; }

        public string CouponCode { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "v.AddTime DESC";
            }
        }
    }
}