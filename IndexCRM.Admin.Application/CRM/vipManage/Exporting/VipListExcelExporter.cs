using System.Collections.Generic;
using System.Linq;
using Abp.Collections.Extensions;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using IndexCRM.Admin.Authorization.Users.Dto;
using IndexCRM.Admin.CRM.vipManage.Dto;
using IndexCRM.Admin.DataExporting.Excel.EpPlus;
using IndexCRM.Admin.Dto;

namespace IndexCRM.Admin.CRM.vipManage.Exporting
{
    public class VipListExcelExporter : EpPlusExcelExporterBase, IVipListExcelExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public VipListExcelExporter(
            ITimeZoneConverter timeZoneConverter, 
            IAbpSession abpSession)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<VipListDto> userListDtos)
        {
            return CreateExcelPackage(
                "UserList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Users"));
                    sheet.OutLineApplyStyle = true;

                    //AddHeader(
                    //    sheet,
                    //    L("Name"),
                    //    L("Surname"),
                    //    L("UserName"),
                    //    L("PhoneNumber"),
                    //    L("EmailAddress"),
                    //    L("EmailConfirm"),
                    //    L("Roles"),
                    //    L("LastLoginTime"),
                    //    L("Active"),
                    //    L("CreationTime")
                    //    );

                    //AddObjects(
                    //    sheet, 2, userListDtos,
                    //    _ => _.Name,
                    //    _ => _.Surname,
                    //    _ => _.UserName,
                    //    _ => _.PhoneNumber,
                    //    _ => _.EmailAddress,
                    //    _ => _.IsEmailConfirmed,
                    //    _ => _.Roles.Select(r => r.RoleName).JoinAsString(", "),
                    //    _ => _timeZoneConverter.Convert(_.LastLoginTime, _abpSession.TenantId, _abpSession.GetUserId()),
                    //    _ => _.IsActive,
                    //    _ => _timeZoneConverter.Convert(_.CreationTime, _abpSession.TenantId, _abpSession.GetUserId())
                    //    );

                    //Formatting cells

                    var lastLoginTimeColumn = sheet.Column(8);
                    lastLoginTimeColumn.Style.Numberformat.Format = "yyyy-mm-dd";

                    var creationTimeColumn = sheet.Column(10);
                    creationTimeColumn.Style.Numberformat.Format = "yyyy-mm-dd";

                    for (var i = 1; i <= 10; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
