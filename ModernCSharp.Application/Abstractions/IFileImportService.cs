using ModernCSharp.Application.Enums;
using ModernCSharp.Application.Models;

namespace ModernCSharp.Application.Abstractions;

public interface IFileImportService
{
    FileType FileType { get; }
    Task<IResult> ImportAsync(FileImportContext context, CancellationToken cancellationToken = default);
    bool CanImport(FileType fileType) =>  fileType == FileType;
}
