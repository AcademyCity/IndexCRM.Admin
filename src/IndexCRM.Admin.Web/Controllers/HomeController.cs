using System.Web.Mvc;

namespace IndexCRM.Admin.Web.Controllers
{
    public class HomeController : AdminControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}