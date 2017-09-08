using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using IndexCRM.Admin.Web.Areas.Mpa.Models.Common.Modals;
using IndexCRM.Admin.Web.Controllers;

namespace IndexCRM.Admin.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize]
    public class CommonController : AdminControllerBase
    {
        public PartialViewResult LookupModal(LookupModalViewModel model)
        {
            return PartialView("Modals/_LookupModal", model);
        }
    }
}