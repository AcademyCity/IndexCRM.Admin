using Abp.Application.Services;
using IndexCRM.Admin.Dto;
using IndexCRM.Admin.Logging.Dto;

namespace IndexCRM.Admin.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
