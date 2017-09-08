using System.Collections.Generic;
using IndexCRM.Admin.Authorization.Users.Dto;
using IndexCRM.Admin.CRM.vipManage.Dto;
using IndexCRM.Admin.Dto;

namespace IndexCRM.Admin.CRM.vipManage.Exporting
{
    public interface IVipListExcelExporter
    {
        FileDto ExportToFile(List<VipListDto> userListDtos);
    }
}