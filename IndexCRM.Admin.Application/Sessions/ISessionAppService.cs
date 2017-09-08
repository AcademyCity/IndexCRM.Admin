using System.Threading.Tasks;
using Abp.Application.Services;
using IndexCRM.Admin.Sessions.Dto;

namespace IndexCRM.Admin.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
