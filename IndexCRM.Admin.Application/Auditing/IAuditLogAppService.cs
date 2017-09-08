using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using IndexCRM.Admin.Auditing.Dto;
using IndexCRM.Admin.Dto;

namespace IndexCRM.Admin.Auditing
{
    public interface IAuditLogAppService : IApplicationService
    {
        Task<PagedResultDto<AuditLogListDto>> GetAuditLogs(GetAuditLogsInput input);

        Task<FileDto> GetAuditLogsToExcel(GetAuditLogsInput input);
    }
}