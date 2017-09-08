using Abp.Runtime.Validation;
using IndexCRM.Admin.Dto;

namespace IndexCRM.Admin.CRM.vipManage.Dto
{
    public class GetVipInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }

        public string Permission { get; set; }

        public int? Role { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "v.AddTime DESC";
            }
        }
    }
}