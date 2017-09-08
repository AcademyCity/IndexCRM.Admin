namespace IndexCRM.Admin.Web
{
    public interface IWebUrlService
    {
        string GetSiteRootAddress(string tenancyName = null);

        bool SupportsTenancyNameInUrl { get; }
    }
}
