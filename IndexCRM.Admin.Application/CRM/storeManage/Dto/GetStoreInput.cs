using Abp.Runtime.Validation;
using IndexCRM.Admin.Dto;

namespace IndexCRM.Admin.CRM.storeManage.Dto
{
    public class GetStoreInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string StoreId { get; set; }

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