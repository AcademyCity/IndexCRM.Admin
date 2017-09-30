using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using IndexCRM.Admin.Authorization.Users;

namespace IndexCRM.Admin.CRM.couponManage.Dto
{
    [AutoMapFrom(typeof(CouponConfig))]
    public class CouponConfigListDto : EntityDto<string>
    {
        public string Id { get; set; }

        public string CouponName { get; set; }
    }
}