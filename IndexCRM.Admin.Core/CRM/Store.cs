using Abp.Domain.Entities;

namespace IndexCRM.Admin.CRM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Store")]
    public class Store : Entity<string>
    {
        [Column("StoreId")]
        [StringLength(40)]
        public override string Id { get; set; }

        [StringLength(40)]
        public string StoreNo { get; set; }

        public string StoreName { get; set; }

        public string StorePhone { get; set; }

        public string StoreAddr { get; set; }

        public string StoreLocation { get; set; }

        public int? Sort { get; set; }

        public bool? IsShow { get; set; }

        public bool? IsDelete { get; set; }

        public DateTime? AddTime { get; set; }

        [StringLength(40)]
        public string AddMan { get; set; }
        
        public DateTime? ModifyTime { get; set; }

        [StringLength(40)]
        public string ModifyMan { get; set; }
    }
}
