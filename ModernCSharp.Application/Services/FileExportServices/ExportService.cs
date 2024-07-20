using ModernCSharp.Application.Abstractions;
using ModernCSharp.Application.Models;

namespace ModernCSharp.Application.Services.FileExportServices;

public class ExportService(IEnumerable<IFileExportService> strategies)
{
    public async Task<IResult> ExportAsync(FileExportContext context, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var strategy = strategies.FirstOrDefault(x => x.CanExport(context.FileType)) ??
         throw new NotImplementedException($"Cannot find the implementation of {nameof(IFileExportService)} for {context.FileType}");
        return await strategy.ExportAsync(context, cancellationToken);
    }
}