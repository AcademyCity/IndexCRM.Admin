using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using IndexCRM.Admin.Authorization.Users;

namespace IndexCRM.Admin.CRM.vipManage.Dto
{
    [AutoMapFrom(typeof(Vip))]
    public class VipListDto : EntityDto<string>
    {
        public string Id { get; set; }

        public string VipCode { get; set; }

        public string VipName { get; set; }

        public int? VipPoint { get; set; }

        public bool? VipSex { get; set; }

        public DateTime? VipBirthday { get; set; }

        public string VipPhone { get; set; }

        public string VipCountry { get; set; }

        public string VipProvince { get; set; }

        public string VipCity { get; set; }

        public string VipHeadImg { get; set; }

        public string Status { get; set; }

        public DateTime? AddTime { get; set; }

        public string AddMan { get; set; }

        public DateTime? ModifyTime { get; set; }

        public string ModifyMan { get; set; }
    }
}