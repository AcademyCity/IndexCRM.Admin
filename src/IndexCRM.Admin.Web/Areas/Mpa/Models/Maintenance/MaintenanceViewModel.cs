using System.Collections.Generic;
using IndexCRM.Admin.Caching.Dto;

namespace IndexCRM.Admin.Web.Areas.Mpa.Models.Maintenance
{
    public class MaintenanceViewModel
    {
        public IReadOnlyList<CacheDto> Caches { get; set; }
    }
}