using ModernCSharp.Application.Enums;
using ModernCSharp.Application.Models;

namespace ModernCSharp.Application.Utils;

public class FilePathUtils
{
    public static bool ValidatePath(string path) => !path.Contains("..") && Path.IsPathRooted(path);

    public static bool ValidateExportContext(FileExportContext exportContext) => exportContext is not null && !string.IsNullOrEmpty(exportContext.OutputPath);
    public static bool ValidateImportContext(FileImportContext importContext) => importContext is not null && !string.IsNullOrEmpty(importContext.InputPath);

    public static FileType MapToFileType(string fileExtension)
    {
        return fileExtension.ToUpper() switch
        {
            ".JSON" => FileType.JSON,
            ".CSV" => FileType.CSV,
            ".XLSX" => FileType.EXCEL,
            _ => throw new Exception("Unsupported file type")
        };
    }
}
