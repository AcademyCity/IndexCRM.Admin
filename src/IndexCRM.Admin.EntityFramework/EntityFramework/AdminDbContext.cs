using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using IndexCRM.Admin.Authorization.Roles;
using IndexCRM.Admin.Authorization.Users;
using IndexCRM.Admin.Chat;
using IndexCRM.Admin.Friendships;
using IndexCRM.Admin.MultiTenancy;
using IndexCRM.Admin.Storage;

namespace IndexCRM.Admin.EntityFramework
{
    /* Constructors of this DbContext is important and each one has it's own use case.
     * - Default constructor is used by EF tooling on design time.
     * - constructor(nameOrConnectionString) is used by ABP on runtime.
     * - constructor(existingConnection) is used by unit tests.
     * - constructor(existingConnection,contextOwnsConnection) can be used by ABP if DbContextEfTransactionStrategy is used.
     * See http://www.aspnetboilerplate.com/Pages/Documents/EntityFramework-Integration for more.
     */

    public class AdminDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        /* Define an IDbSet for each entity of the application */

        public virtual IDbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual IDbSet<Friendship> Friendships { get; set; }

        public virtual IDbSet<ChatMessage> ChatMessages { get; set; }

        public AdminDbContext()
            : base("Default")
        {
            
        }

        public AdminDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        public AdminDbContext(DbConnection existingConnection)
           : base(existingConnection, false)
        {

        }

        public AdminDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
