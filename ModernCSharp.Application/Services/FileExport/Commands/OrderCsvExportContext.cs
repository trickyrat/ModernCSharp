using ModernCSharp.Application.Enums;
using ModernCSharp.Application.Models.FileExport;

namespace ModernCSharp.Application.Services.FileExport.Commands;

public class OrderCsvExportContext : FileExportContext
{
    public OrderCsvExportContext(FileType fileType, string outputPath)
        : base(fileType, outputPath)
    {
    }
}
