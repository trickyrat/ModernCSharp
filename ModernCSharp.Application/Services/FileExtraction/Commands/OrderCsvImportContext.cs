using ModernCSharp.Application.Enums;
using ModernCSharp.Application.Models.FileExtraction;

namespace ModernCSharp.Application.Services.FileExtraction.Commands;

public class OrderCsvImportContext : FileImportContext
{
    public OrderCsvImportContext(FileType fileType, string inputPath)
        : base(fileType, inputPath)
    {
    }
}
