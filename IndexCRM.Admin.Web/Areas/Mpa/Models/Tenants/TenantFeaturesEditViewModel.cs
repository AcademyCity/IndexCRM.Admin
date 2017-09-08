using Abp.AutoMapper;
using IndexCRM.Admin.MultiTenancy;
using IndexCRM.Admin.MultiTenancy.Dto;
using IndexCRM.Admin.Web.Areas.Mpa.Models.Common;

namespace IndexCRM.Admin.Web.Areas.Mpa.Models.Tenants
{
    [AutoMapFrom(typeof (GetTenantFeaturesForEditOutput))]
    public class TenantFeaturesEditViewModel : GetTenantFeaturesForEditOutput, IFeatureEditViewModel
    {
        public Tenant Tenant { get; set; }

        public TenantFeaturesEditViewModel(Tenant tenant, GetTenantFeaturesForEditOutput output)
        {
            Tenant = tenant;
            output.MapTo(this);
        }
    }
}