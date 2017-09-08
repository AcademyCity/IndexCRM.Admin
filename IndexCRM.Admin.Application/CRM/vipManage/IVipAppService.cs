using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using IndexCRM.Admin.CRM.vipManage.Dto;
using IndexCRM.Admin.Dto;

namespace IndexCRM.Admin.CRM.vipManage
{
    public interface IVipAppService : IApplicationService
    {
        Task<PagedResultDto<VipListDto>> GetVipList(GetVipInput input);

        Task<FileDto> GetVipListToExcel();

    }
}