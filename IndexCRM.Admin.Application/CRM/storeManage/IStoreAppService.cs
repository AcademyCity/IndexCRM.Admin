using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using IndexCRM.Admin.CRM.storeManage.Dto;

namespace IndexCRM.Admin.CRM.storeManage
{
    public interface IStoreAppService : IApplicationService
    {

        Task<GetStoreForEditDto> GetStoreForEdit(GetStoreInput input);

        Task CreateOrUpdateStore(GetStoreForEditInput input);

        Task<PagedResultDto<GetStoreForEditDto>> GetStoreList(GetStoreInput input);

        Task DeleteStore(GetStoreInput input);

    }
}