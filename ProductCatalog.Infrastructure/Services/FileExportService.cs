using OfficeOpenXml;
using ProductCatalog.Core.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace ProductCatalog.Infrastructure.Services
{
    public class FileExportService : IFileExportService
    {
        public MemoryStream ExportExcel<TEntityVM>(IEnumerable<TEntityVM> entityVMs, string worksheetName)
        {
            var stream = new MemoryStream();
            
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var excelPackage = new ExcelPackage(stream))
            {
                var workSheet = excelPackage.Workbook.Worksheets.Add(worksheetName);
                workSheet.Cells.LoadFromCollection(entityVMs, true);
                excelPackage.Save();
            }

            stream.Position = 0;

            return stream;
        }
    }
}
