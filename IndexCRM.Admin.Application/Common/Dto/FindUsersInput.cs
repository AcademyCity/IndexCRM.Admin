using IndexCRM.Admin.Dto;

namespace IndexCRM.Admin.Common.Dto
{
    public class FindUsersInput : PagedAndFilteredInputDto
    {
        public int? TenantId { get; set; }
    }
}