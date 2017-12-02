using Abp.AutoMapper;
using System;

namespace IndexCRM.Admin.CRM.storeManage.Dto
{
    [AutoMap(typeof(Store))]
    public class GetStoreForEditDto
    {
        public string Id { get; set; }

        public string StoreNo { get; set; }

        public string StoreName { get; set; }

        public string StorePhone { get; set; }

        public string StoreAddr { get; set; }

        public string StoreLocation { get; set; }

        public int? Sort { get; set; }

        public bool? IsShow { get; set; }

        public DateTime? AddTime { get; set; }

        public string AddMan { get; set; }

        public DateTime? ModifyTime { get; set; }

        public string ModifyMan { get; set; }

    }
}