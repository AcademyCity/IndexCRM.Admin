using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using IndexCRM.Admin.Authorization;
using IndexCRM.Admin.Notifications;
using IndexCRM.Admin.Web.Controllers;

namespace IndexCRM.Admin.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize]
    public class NotificationsController : AdminControllerBase
    {
        private readonly INotificationAppService _notificationApp;

        public NotificationsController(INotificationAppService notificationApp)
        {
            _notificationApp = notificationApp;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<PartialViewResult> SettingsModal()
        {
            var notificationSettings = await _notificationApp.GetNotificationSettings();
            return PartialView("_SettingsModal", notificationSettings);
        }
    }
}