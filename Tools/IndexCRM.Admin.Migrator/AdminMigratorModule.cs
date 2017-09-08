using System.Data.Entity;
using System.Reflection;
using Abp.Events.Bus;
using Abp.Modules;
using Castle.MicroKernel.Registration;
using IndexCRM.Admin.EntityFramework;

namespace IndexCRM.Admin.Migrator
{
    [DependsOn(typeof(AdminDataModule))]
    public class AdminMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<AdminDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(typeof(IEventBus), () =>
            {
                IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                );
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}