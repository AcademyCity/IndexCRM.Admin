using Abp.Dependency;
using Abp.Runtime.Session;
using Abp.Web.Mvc.Views;

namespace IndexCRM.Admin.Web.Views
{
    public abstract class AdminWebViewPageBase : AdminWebViewPageBase<dynamic>
    {
       
    }

    public abstract class AdminWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        public IAbpSession AbpSession { get; private set; }
        
        protected AdminWebViewPageBase()
        {
            AbpSession = IocManager.Instance.Resolve<IAbpSession>();
            LocalizationSourceName = AdminConsts.LocalizationSourceName;
        }
    }
}