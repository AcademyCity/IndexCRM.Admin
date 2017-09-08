using Abp.Notifications;
using IndexCRM.Admin.Dto;

namespace IndexCRM.Admin.Notifications.Dto
{
    public class GetUserNotificationsInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }
    }
}