using Abp.WebApi.Controllers;

namespace IndexCRM.Admin.WebApi
{
    public abstract class AdminApiControllerBase : AbpApiController
    {
        protected AdminApiControllerBase()
        {
            LocalizationSourceName = AdminConsts.LocalizationSourceName;
        }
    }
}