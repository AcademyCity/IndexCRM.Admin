using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using IndexCRM.Admin.Editions.Dto;

namespace IndexCRM.Admin.Editions
{
    public interface IEditionAppService : IApplicationService
    {
        Task<ListResultDto<EditionListDto>> GetEditions();

        Task<GetEditionForEditOutput> GetEditionForEdit(NullableIdDto input);

        Task CreateOrUpdateEdition(CreateOrUpdateEditionDto input);

        Task DeleteEdition(EntityDto input);

        Task<List<ComboboxItemDto>> GetEditionComboboxItems(int? selectedEditionId = null);
    }
}