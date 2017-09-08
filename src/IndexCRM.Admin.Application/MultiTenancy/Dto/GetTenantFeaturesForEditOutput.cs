using System.Collections.Generic;
using Abp.Application.Services.Dto;
using IndexCRM.Admin.Editions.Dto;

namespace IndexCRM.Admin.MultiTenancy.Dto
{
    public class GetTenantFeaturesForEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}