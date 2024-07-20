using ModernCSharp.Application.Enums;
using ModernCSharp.Application.Models;

namespace ModernCSharp.Application.Abstractions;

public interface IFileExportService
{
    FileType FileType { get; }
    Task<IResult> ExportAsync(FileExportContext context, CancellationToken cancellationToken = default);
    bool CanExport(FileType fileType) => FileType == fileType;
}