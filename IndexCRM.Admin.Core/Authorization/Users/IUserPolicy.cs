using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace IndexCRM.Admin.Authorization.Users
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}
