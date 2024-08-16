using ModernCSharp.Application.Enums;

namespace ModernCSharp.Application.Models.FileExport;

public interface IFileExportContext
{
    string OutputPath
    {
        get; set;
    }

    FileType FileType
    {
        get; set;
    }
}
