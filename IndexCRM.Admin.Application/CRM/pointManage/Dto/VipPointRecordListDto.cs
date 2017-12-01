using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using IndexCRM.Admin.Authorization.Users;

namespace IndexCRM.Admin.CRM.pointManage.Dto
{
    [AutoMap(typeof(PointRecord))]
    public class VipPointRecordListDto : EntityDto<string>
    {
        public int? PointChange { get; set; }

        public string PointExplain { get; set; }

        public string PosNo { get; set; }

        public DateTime? AddTime { get; set; }

        public string AddMan { get; set; }

        public string VipCode { get; set; }

        public string VipId { get; set; }
    }
}