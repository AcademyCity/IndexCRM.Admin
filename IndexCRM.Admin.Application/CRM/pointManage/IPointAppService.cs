using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using IndexCRM.Admin.CRM.pointManage.Dto;

namespace IndexCRM.Admin.CRM.pointManage
{
    public interface IPointAppService : IApplicationService
    {
        Task<PagedResultDto<VipPointRecordListDto>> GetVipPointRecordList(GetVipPointRecordInput input);

        Task ChangePoint(ChangePointInput input);

        Task SendPoint(ChangePointInput input);

        Task<PagedResultDto<VipPointRecordListDto>> GetSendPointRecordList(GetVipPointRecordInput input);
    }
}