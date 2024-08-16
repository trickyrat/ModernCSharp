using ModernCSharp.Application.Enums;
using ModernCSharp.Application.Models.FileExport;

namespace ModernCSharp.Application.Services.FileExport.Commands;

public class OrderJsonExportContext : FileExportContext
{
    public OrderJsonExportContext(FileType fileType, string outputPath)
        : base(fileType, outputPath)
    {
    }
}
