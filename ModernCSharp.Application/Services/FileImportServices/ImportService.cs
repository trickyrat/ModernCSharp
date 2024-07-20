using ModernCSharp.Application.Abstractions;
using ModernCSharp.Application.Models;

namespace ModernCSharp.Application.Services.FileImportServices;

public class ImportService(IEnumerable<IFileImportService> strategies)
{
    public async Task<IResult> ImportAsync(FileImportContext context, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var strategy = strategies.FirstOrDefault(x => x.CanImport(context.FileType)) ??
         throw new NotImplementedException($"Cannot find the implementation of {nameof(IFileImportService)} for {context.FileType}");
        return await strategy.ImportAsync(context, cancellationToken);
    }
}
