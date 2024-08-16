using ModernCSharp.Application.Enums;

namespace ModernCSharp.Application.Models.FileExport;

public class FileExportContext : IFileExportContext
{
    public string OutputPath
    {
        get;
        set;
    }
    public FileType FileType
    {
        get;
        set;
    }

    public FileExportContext(FileType fileType, string outputPath)
    {
        FileType = fileType;
        OutputPath = outputPath;
    }
}