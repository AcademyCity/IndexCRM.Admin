using Abp.Runtime.Validation;
using IndexCRM.Admin.Dto;

namespace IndexCRM.Admin.CRM.pointManage.Dto
{
    public class GetVipPointRecordInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string VipId { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "AddTime DESC";
            }
        }
    }
}