using System.Web.Mvc;
using Abp.Auditing;
using Abp.Web.Mvc.Authorization;
using IndexCRM.Admin.Authorization;
using IndexCRM.Admin.Web.Controllers;

namespace IndexCRM.Admin.Web.Areas.Mpa.Controllers
{
    [DisableAuditing]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_AuditLogs)]
    public class AuditLogsController : AdminControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}