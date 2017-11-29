using Abp.Runtime.Validation;
using IndexCRM.Admin.Dto;

namespace IndexCRM.Admin.CRM.couponManage.Dto
{
    public class GetCouponConfigInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string CouponConfigId { get; set; }

        public string Filter { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "AddTime DESC";
            }
        }
    }
}