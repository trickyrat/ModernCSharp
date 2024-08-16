using ModernCSharp.Application.Enums;
using ModernCSharp.Application.Models.FileExport;

namespace ModernCSharp.Application.Services.FileExport.Commands;
public class OrderExcelExportContext : FileExportContext
{
    public OrderExcelExportContext(FileType fileType, string outputPath)
        : base(fileType, outputPath)
    {
    }
}
