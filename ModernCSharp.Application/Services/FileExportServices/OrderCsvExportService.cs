using CsvHelper;

using Microsoft.Extensions.Logging;
using ModernCSharp.Application.Abstractions;
using ModernCSharp.Application.Enums;
using ModernCSharp.Application.Models;
using ModernCSharp.Application.Models.Errors;
using ModernCSharp.Application.Utils;
using System.Globalization;

namespace ModernCSharp.Application.Services.FileExportServices;

public class OrderCsvExportService(ILogger<OrderCsvExportService> logger, OrderService orderService) : IFileExportService
{
    public FileType FileType => FileType.CSV;

    public async Task<IResult> ExportAsync(FileExportContext context, CancellationToken cancellationToken = default)
    {
        if (!FilePathUtils.ValidateExportContext(context))
        {
            logger.LogWarning("Invalid export context provided.");
            return Result.Failure(FileExportErrors.InvalidContext);
        }

        try
        {
            if (!FilePathUtils.ValidatePath(context.OutputPath))
            {
                return Result.Failure(FileExportErrors.InvalidOutputPath);
            }

            await PerformExportAsync(context, cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while exporting orders to {@OutputPath}.", context.OutputPath);
            return Result.Failure(new("", $"An error occurred while exporting orders to {context.OutputPath}."));
        }
    }

    private async Task PerformExportAsync(FileExportContext context, CancellationToken cancellationToken = default)
    {
        var orders = await orderService.GetOrdersAsync(cancellationToken);

        if (orders.Count == 0)
        {
            logger.LogInformation("No order to export.");
            return;
        }

        using var stream = new FileStream(context.OutputPath, FileMode.Create, FileAccess.Write, FileShare.Read);
        using var writer = new StreamWriter(stream);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

        await csv.WriteRecordsAsync(orders, cancellationToken);
    }
}
