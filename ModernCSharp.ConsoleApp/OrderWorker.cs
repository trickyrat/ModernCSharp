using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using ModernCSharp.Application.Enums;
using ModernCSharp.Application.Services.FileExport;
using ModernCSharp.Application.Services.FileExport.Commands;
using ModernCSharp.Application.Services.FileExtraction;
using ModernCSharp.Application.Services.FileExtraction.Commands;


namespace ModernCSharp.ConsoleApp;

public class OrderWorker(ILogger<OrderWorker> logger
    , FileExporterFactory exporterFactory
    , FileImporterFactory importerFactory) : BackgroundService
{
    private int _exitCode = 0;

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {

            var result = await ImportAsync(stoppingToken);

            if (result) { await ExportAsync(stoppingToken); }
        }
        catch (Exception ex)
        {
            _exitCode = 500;
            logger.LogError(ex, "An error occurred: {@Message}", ex.Message);
            throw;
        }
        finally
        {
            Environment.Exit(_exitCode);
        }
    }

    private async Task<bool> ExportAsync(CancellationToken cancellationToken = default)
    {
        var csvExportContext = new OrderCsvExportContext(FileType.CSV, "D:\\orders.csv");
        var csvExporter = exporterFactory.Create(csvExportContext);
        var result = await csvExporter.ExportAsync(cancellationToken);

        if (result.IsSuccess)
        {
            var jsonExportContext = new OrderJsonExportContext(FileType.JSON, "D:\\orders.json");
            var jsonExporter = exporterFactory.Create(jsonExportContext);
            result = await jsonExporter.ExportAsync(cancellationToken);
        }

        if (result.IsSuccess)
        {
            var excelExportContext = new OrderExcelExportContext(FileType.EXCEL, "D:\\orders.xlsx");
            var excelExporter = exporterFactory.Create(excelExportContext);
            result = await excelExporter.ExportAsync(cancellationToken);
        }

        if (result.IsFailure)
        {
            logger.LogError("Failed to export files: {@Message}", result.Error);
        }
        return result.IsSuccess;
    }

    private async Task<bool> ImportAsync(CancellationToken cancellationToken = default)
    {
        var csvImportContext = new OrderCsvImportContext(FileType.CSV, "D:\\additional orders.csv");
        var csvImporter = importerFactory.Create(csvImportContext);
        var result = await csvImporter.ImportAsync(cancellationToken);

        if (result.IsSuccess)
        {
            var jsonImportContext = new OrderJsonImportContext(FileType.JSON, "D:\\additional orders.json");
            var jsonImporter = importerFactory.Create(jsonImportContext);
            result = await jsonImporter.ImportAsync(cancellationToken);
        }

        if (result.IsSuccess)
        {
            var excelImportContext = new OrderExcelImportContext(FileType.EXCEL, "D:\\additional orders.xlsx");
            var excelImporter = importerFactory.Create(excelImportContext);
            result = await excelImporter.ImportAsync(cancellationToken);
        }

        if (result.IsFailure)
        {
            logger.LogError("Failed to import files: {@Message}", result.Error);
        }
        return result.IsSuccess;
    }
}
