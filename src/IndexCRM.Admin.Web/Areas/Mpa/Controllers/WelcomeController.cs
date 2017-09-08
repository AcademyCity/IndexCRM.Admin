using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using IndexCRM.Admin.Web.Controllers;

namespace IndexCRM.Admin.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize]
    public class WelcomeController : AdminControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}