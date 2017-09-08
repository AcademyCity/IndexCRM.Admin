using System.Threading.Tasks;
using IndexCRM.Admin.Sessions.Dto;

namespace IndexCRM.Admin.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
