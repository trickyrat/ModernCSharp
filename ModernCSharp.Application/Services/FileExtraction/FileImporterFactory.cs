using ModernCSharp.Application.Abstractions;
using ModernCSharp.Application.Models.FileExtraction;

namespace ModernCSharp.Application.Services.FileExtraction;

public class FileImporterFactory(IEnumerable<IFileImporter> importers)
{
    public IFileImporter Create(IFileImportContext context)
    {
        var fileImporter = importers.FirstOrDefault(x => x.CanImport(context.FileType)) ??
            throw new NotImplementedException($"Cannot find the implementation of {nameof(IFileImporter)} for {context.FileType}");

        fileImporter.InitializeContext(context);
        return fileImporter;
    }
}
