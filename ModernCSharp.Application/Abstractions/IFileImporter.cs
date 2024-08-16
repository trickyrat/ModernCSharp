using ModernCSharp.Application.Enums;
using ModernCSharp.Application.Models;
using ModernCSharp.Application.Models.FileExtraction;

namespace ModernCSharp.Application.Abstractions;

public interface IFileImporter
{
    List<FileType> FileTypes { get; }
    void InitializeContext(IFileImportContext context);
    Task<IResult> ImportAsync(CancellationToken cancellationToken = default);
    bool CanImport(FileType fileType) => FileTypes.Contains(fileType);
}
