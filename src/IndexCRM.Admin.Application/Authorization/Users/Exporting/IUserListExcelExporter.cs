using System.Collections.Generic;
using IndexCRM.Admin.Authorization.Users.Dto;
using IndexCRM.Admin.Dto;

namespace IndexCRM.Admin.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}