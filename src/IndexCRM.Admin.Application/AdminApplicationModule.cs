using System.Reflection;
using Abp.AutoMapper;
using Abp.Localization;
using Abp.Modules;
using IndexCRM.Admin.Authorization;

namespace IndexCRM.Admin
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(typeof(AdminCoreModule))]
    public class AdminApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper mappings
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                CustomDtoMapper.CreateMappings(mapper);
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
