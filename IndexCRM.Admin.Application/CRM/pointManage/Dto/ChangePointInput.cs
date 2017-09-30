using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.Runtime.Validation;
using IndexCRM.Admin.Dto;

namespace IndexCRM.Admin.CRM.pointManage.Dto
{
    public class ChangePointInput 
    {
        [DisplayName("会员ID")]
        public string VipId { get; set; }

        [DisplayName("修改数量")]
        [Range(-100000000, 100000000)]
        public int Amount { get; set; }

        [DisplayName("修改原因")]
        public string Explain { get; set; }

    }
}