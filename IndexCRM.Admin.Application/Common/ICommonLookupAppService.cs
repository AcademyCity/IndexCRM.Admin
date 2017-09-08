using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using IndexCRM.Admin.Common.Dto;

namespace IndexCRM.Admin.Common
{
    public interface ICommonLookupAppService : IApplicationService
    {
        Task<ListResultDto<ComboboxItemDto>> GetEditionsForCombobox();

        Task<PagedResultDto<NameValueDto>> FindUsers(FindUsersInput input);

        string GetDefaultEditionName();
    }
}