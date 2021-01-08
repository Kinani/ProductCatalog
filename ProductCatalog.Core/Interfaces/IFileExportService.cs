using System.Collections.Generic;
using System.IO;

namespace ProductCatalog.Core.Interfaces
{
    public interface IFileExportService
    {
        MemoryStream ExportExcel<TEntityVM>(IEnumerable<TEntityVM> entityVMs, string worksheetName);
    }
}
