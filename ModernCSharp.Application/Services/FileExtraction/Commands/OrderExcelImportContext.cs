using ModernCSharp.Application.Enums;
using ModernCSharp.Application.Models.FileExtraction;

namespace ModernCSharp.Application.Services.FileExtraction.Commands;

public class OrderExcelImportContext : FileImportContext
{
    public OrderExcelImportContext(FileType fileType, string inputPath) 
        : base(fileType, inputPath)
    {

    }
}
