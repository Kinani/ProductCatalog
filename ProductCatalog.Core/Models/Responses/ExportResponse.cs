using System.IO;

namespace ProductCatalog.Core.Models.Responses
{
    public class ExportResponse : BaseResponse<MemoryStream>
    {
        public ExportResponse(MemoryStream memoryStream) : base(memoryStream) { }

        public ExportResponse(string message) : base(message) { }
    }
}
