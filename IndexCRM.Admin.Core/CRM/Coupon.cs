using Abp.Domain.Entities;

namespace IndexCRM.Admin.CRM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Coupon")]
    public class Coupon : Entity<string>
    {
        [Column("CouponId")]
        [StringLength(40)]
        public override string Id { get; set; }

        [StringLength(40)]
        public string CouponConfigId { get; set; }

        [StringLength(40)]
        public string VipId { get; set; }

        [StringLength(16)]
        public string CouponCode { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime? AddTime { get; set; }

        [StringLength(40)]
        public string AddMan { get; set; }

        [StringLength(32)]
        public string AddExplain { get; set; }

        public bool IsUse { get; set; }

        public DateTime? ModifyTime { get; set; }

        [StringLength(40)]
        public string ModifyMan { get; set; }
    }
}
