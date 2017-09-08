using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using IndexCRM.Admin.Authorization.Users;
using IndexCRM.Admin.MultiTenancy;

namespace IndexCRM.Admin.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}
