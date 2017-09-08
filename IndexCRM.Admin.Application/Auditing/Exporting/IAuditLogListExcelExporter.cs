using System.Collections.Generic;
using IndexCRM.Admin.Auditing.Dto;
using IndexCRM.Admin.Dto;

namespace IndexCRM.Admin.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);
    }
}
