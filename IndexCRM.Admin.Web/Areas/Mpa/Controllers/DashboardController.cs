using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using IndexCRM.Admin.Authorization;
using IndexCRM.Admin.Web.Controllers;

namespace IndexCRM.Admin.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Tenant_Dashboard)]
    public class DashboardController : AdminControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}