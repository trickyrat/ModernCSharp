using ModernCSharp.Application.Enums;

namespace ModernCSharp.Application.Models.FileExtraction;

public interface IFileImportContext
{
    FileType FileType
    {
        get; set;
    }
    string InputPath
    {
        get; set;
    }
}

