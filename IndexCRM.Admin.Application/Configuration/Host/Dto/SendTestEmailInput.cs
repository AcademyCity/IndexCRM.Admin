using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using IndexCRM.Admin.Authorization.Users;

namespace IndexCRM.Admin.Configuration.Host.Dto
{
    public class SendTestEmailInput
    {
        [Required]
        [MaxLength(User.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }
    }
}