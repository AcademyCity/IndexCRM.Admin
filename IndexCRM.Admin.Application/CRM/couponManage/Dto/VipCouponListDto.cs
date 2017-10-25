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
    [AutoMapFrom(typeof(Coupon))]
    public class VipCouponListDto : EntityDto<string>
    {
        public string CouponConfigId { get; set; }

        public string CouponName { get; set; }

        public string CouponCode { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public bool? IsUse { get; set; }

        public DateTime? ModifyTime { get; set; }

        public string ModifyMan { get; set; }
    }
}