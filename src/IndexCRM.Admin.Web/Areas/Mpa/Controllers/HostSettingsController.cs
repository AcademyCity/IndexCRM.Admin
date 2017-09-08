using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Configuration;
using Abp.Runtime.Session;
using Abp.Timing;
using Abp.Web.Mvc.Authorization;
using IndexCRM.Admin.Authorization;
using IndexCRM.Admin.Authorization.Users;
using IndexCRM.Admin.Configuration.Host;
using IndexCRM.Admin.Editions;
using IndexCRM.Admin.Timing;
using IndexCRM.Admin.Timing.Dto;
using IndexCRM.Admin.Web.Areas.Mpa.Models.HostSettings;
using IndexCRM.Admin.Web.Controllers;

namespace IndexCRM.Admin.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Host_Settings)]
    public class HostSettingsController : AdminControllerBase
    {
        private readonly UserManager _userManager;
        private readonly IHostSettingsAppService _hostSettingsAppService;
        private readonly IEditionAppService _editionAppService;
        private readonly ITimingAppService _timingAppService;

        public HostSettingsController(
            IHostSettingsAppService hostSettingsAppService,
            UserManager userManager, 
            IEditionAppService editionAppService, 
            ITimingAppService timingAppService)
        {
            _hostSettingsAppService = hostSettingsAppService;
            _userManager = userManager;
            _editionAppService = editionAppService;
            _timingAppService = timingAppService;
        }

        public async Task<ActionResult> Index()
        {
            var hostSettings = await _hostSettingsAppService.GetAllSettings();
            var editionItems = await _editionAppService.GetEditionComboboxItems(hostSettings.TenantManagement.DefaultEditionId);
            var timezoneItems = await _timingAppService.GetTimezoneComboboxItems(new GetTimezoneComboboxItemsInput
            {
                DefaultTimezoneScope = SettingScopes.Application,
                SelectedTimezoneId = await SettingManager.GetSettingValueForApplicationAsync(TimingSettingNames.TimeZone)
            });

            ViewBag.CurrentUserEmail = await _userManager.GetEmailAsync(AbpSession.GetUserId());

            var model = new HostSettingsViewModel
            {
                Settings = hostSettings,
                EditionItems = editionItems,
                TimezoneItems = timezoneItems
            };

            return View(model);
        }
    }
}