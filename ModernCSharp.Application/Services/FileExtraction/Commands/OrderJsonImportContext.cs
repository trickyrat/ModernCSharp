using ModernCSharp.Application.Enums;
using ModernCSharp.Application.Models.FileExtraction;

namespace ModernCSharp.Application.Services.FileExtraction.Commands;

public class OrderJsonImportContext : FileImportContext
{
    public OrderJsonImportContext(FileType fileType, string inputPath)
        : base(fileType, inputPath)
    {
    }
}
