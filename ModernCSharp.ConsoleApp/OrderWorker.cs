using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModernCSharp.ConsoleApp.Enums;
using ModernCSharp.ConsoleApp.Models;
using ModernCSharp.ConsoleApp.Services.FileExportServices;
using ModernCSharp.ConsoleApp.Services.FileImportServices;

namespace ModernCSharp.ConsoleApp;

public class OrderWorker(ILogger<OrderWorker> logger, ExportService exportService, ImportService importService) : BackgroundService
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
        var csvExportContext = new FileExportContext(FileType: FileType.CSV, OutputPath: "D:\\orders.csv");
        var result = await exportService.ExportAsync(csvExportContext, cancellationToken);

        if (result.IsSuccess)
        {
            var jsonExportContext = new FileExportContext(FileType: FileType.JSON, OutputPath: "D:\\orders.json");
            result = await exportService.ExportAsync(jsonExportContext, cancellationToken);
        }

        if (result.IsSuccess)
        {
            var excelExportContext = new FileExportContext(FileType: FileType.EXCEL, OutputPath: "D:\\orders.xlsx");
            result = await exportService.ExportAsync(excelExportContext, cancellationToken);
        }

        if (result.IsFailure)
        {
            logger.LogError("Failed to export files: {@Message}", result.Error);
        }
        return result.IsSuccess;
    }

    private async Task<bool> ImportAsync(CancellationToken cancellationToken = default)
    {
        var csvImportContext = new FileImportContext(FileType: FileType.CSV, InputPath: "D:\\additional orders.csv");
        var result = await importService.ImportAsync(csvImportContext, cancellationToken);

        if (result.IsSuccess)
        {
            var jsonImportContext = new FileImportContext(FileType: FileType.JSON, InputPath: "D:\\additional orders.json");
            await importService.ImportAsync(jsonImportContext, cancellationToken);
        }

        if (result.IsSuccess)
        {
            var excelImportContext = new FileImportContext(FileType: FileType.EXCEL, InputPath: "D:\\additional orders.xlsx");
            await importService.ImportAsync(excelImportContext, cancellationToken);
        }

        if (result.IsFailure)
        {
            logger.LogError("Failed to import files: {@Message}", result.Error);
        }
        return result.IsSuccess;
    }
}
