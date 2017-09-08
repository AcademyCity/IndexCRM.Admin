using EntityFramework.DynamicFilters;
using IndexCRM.Admin.EntityFramework;

namespace IndexCRM.Admin.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly AdminDbContext _context;
        private readonly int _tenantId;

        public TestDataBuilder(AdminDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new TestOrganizationUnitsBuilder(_context, _tenantId).Create();

            _context.SaveChanges();
        }
    }
}
