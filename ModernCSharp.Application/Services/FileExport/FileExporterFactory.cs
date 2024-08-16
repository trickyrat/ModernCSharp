using ModernCSharp.Application.Abstractions;
using ModernCSharp.Application.Models.FileExport;

namespace ModernCSharp.Application.Services.FileExport;

public class FileExporterFactory(IEnumerable<IFileExporter> strategies)
{
    public IFileExporter Create(IFileExportContext context)
    {
        var exporter = strategies.FirstOrDefault(x => x.CanExport(context.FileType)) ??
         throw new NotImplementedException($"Cannot find the implementation of {nameof(IFileExporter)} for {context.FileType}");
        exporter.InitializeContext(context);
        return exporter;
    }
}