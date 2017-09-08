using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Notifications;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Zero.Configuration;
using IndexCRM.Admin.Authorization;
using Microsoft.AspNet.Identity;
using IndexCRM.Admin.Authorization.Permissions;
using IndexCRM.Admin.Authorization.Permissions.Dto;
using IndexCRM.Admin.Authorization.Roles;
using IndexCRM.Admin.Authorization.Users;
using IndexCRM.Admin.Authorization.Users.Exporting;
using IndexCRM.Admin.CRM.vipManage;
using IndexCRM.Admin.Dto;
using IndexCRM.Admin.Features;
using IndexCRM.Admin.Notifications;
using IndexCRM.Admin.CRM.vipManage.Dto;
using IndexCRM.Admin.CRM.vipManage.Exporting;

namespace IndexCRM.Admin.CRM.vipManage
{
    [AbpAuthorize(AppPermissions.CRM_VipManage)]
    public class VipAppService : AdminAppServiceBase, IVipAppService
    {
        private readonly RoleManager _roleManager;

        private readonly IVipListExcelExporter _vipListExcelExporter;
        private readonly IRepository<RolePermissionSetting, long> _rolePermissionRepository;
        private readonly IRepository<UserPermissionSetting, long> _userPermissionRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;

        private readonly IRepository<Vip, string> _vip;
        private readonly IRepository<Point, string> _point;

        public VipAppService(
            RoleManager roleManager,
            IVipListExcelExporter vipListExcelExporter,
            IRepository<RolePermissionSetting, long> rolePermissionRepository,
            IRepository<UserPermissionSetting, long> userPermissionRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<Vip, string> vip,
            IRepository<Point, string> point)
        {
            _roleManager = roleManager;
            _vipListExcelExporter = vipListExcelExporter;
            _rolePermissionRepository = rolePermissionRepository;
            _userPermissionRepository = userPermissionRepository;
            _userRoleRepository = userRoleRepository;
            _vip = vip;
            _point = point;
        }

        [AbpAuthorize(AppPermissions.CRM_VipManage)]
        public async Task<PagedResultDto<VipListDto>> GetVipList(GetVipInput input)
        {
            var vip = _vip.GetAll()
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.VipCode.Contains(input.Filter) ||
                        u.VipPhone.Contains(input.Filter)
                );

            var query = from v in vip
                        join p in _point.GetAll() on v.Id equals p.VipId
                        select new
                        {
                            v,
                            p = p == null ? null : new { p.VipId, p.VipPoint }
                        };

            var vipCount = await query.CountAsync();
            var vipList = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            var vipListDtos = vipList.Select(s => s.v).MapTo<List<VipListDto>>();

            vipListDtos = vipListDtos.Select(v =>
            {
                var dto = v;
                dto.VipPoint =
                    vipList.Select(s => s.p)
                        .Where(w => w.VipId == v.Id)
                        .Select(si => si.VipPoint)
                        .FirstOrDefault();
                return dto;
            }).ToList();

            return new PagedResultDto<VipListDto>(
                vipCount,
                vipListDtos
                );
        }

        public async Task<FileDto> GetVipListToExcel()
        {
            var users = await UserManager.Users.Include(u => u.Roles).ToListAsync();
            var vipListDtos = users.MapTo<List<VipListDto>>();

            return _vipListExcelExporter.ExportToFile(vipListDtos);
        }
    }
}
