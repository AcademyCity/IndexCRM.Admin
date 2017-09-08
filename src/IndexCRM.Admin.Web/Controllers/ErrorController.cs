using System.Web.Mvc;
using Abp.Auditing;

namespace IndexCRM.Admin.Web.Controllers
{
    public class ErrorController : AdminControllerBase
    {
        [DisableAuditing]
        public ActionResult E404()
        {
            return View();
        }
    }
}