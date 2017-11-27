using System;

namespace IndexCRM.Admin.CRM.couponManage.Dto
{
    public class GetCouponConfigForEditOutput
    {
        public string Id { get; set; }

        public string CouponName { get; set; }

        public string CouponImg { get; set; }

        public int? CouponPoint { get; set; }

        public string CouponExplain { get; set; }

        public int? CouponNum { get; set; }

        public string ValidityMode { get; set; }

        public string PosKey { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int? EffectDate { get; set; }

        public int? ValidDate { get; set; }

        public int? Sort { get; set; }

        public bool? IsShow { get; set; }

    }
}