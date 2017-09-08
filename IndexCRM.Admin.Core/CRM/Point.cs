using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using Abp.Domain.Entities;

namespace IndexCRM.Admin.CRM
{
    [Table("Point")]
    public class Point : Entity<string>
    {
        [Column("PointId")]
        [StringLength(40)]
        public override string Id { get; set; }

        [StringLength(40)]
        public string VipId { get; set; }

        public int? VipPoint { get; set; }
    }
}
