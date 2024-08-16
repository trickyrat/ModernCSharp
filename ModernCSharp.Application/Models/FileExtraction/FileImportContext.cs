using ModernCSharp.Application.Enums;

namespace ModernCSharp.Application.Models.FileExtraction;

public class FileImportContext : IFileImportContext
{

    public string InputPath
    {
        get;
        set;
    }
    public FileType FileType
    {
        get;
        set;
    }
    public FileImportContext(FileType fileType, string inputPath)
    {
        FileType = fileType;
        InputPath = inputPath;
    }
}
