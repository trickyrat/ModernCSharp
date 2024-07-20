using ModernCSharp.Application.Abstractions;
using ModernCSharp.Application.Enums;
using ModernCSharp.Application.Models;
using ModernCSharp.Application.Models.Errors;

using CsvHelper;

using Microsoft.Extensions.Logging;
using ModernCSharp.Application.Utils;
using System.Globalization;

namespace ModernCSharp.Application.Services.FileImportServices;

public class OrderCsvImportService(ILogger<OrderCsvImportService> logger, OrderService orderService) : IFileImportService
{
    public FileType FileType => FileType.CSV;

    public async Task<IResult> ImportAsync(FileImportContext context, CancellationToken cancellationToken = default)
    {
        if (!FilePathUtils.ValidateImportContext(context))
        {
            logger.LogWarning("Invalid import context provided.");
            return Result.Failure(FileImportErrors.InvalidContext);
        }

        try
        {
            if (!FilePathUtils.ValidatePath(context.InputPath))
            {
                return Result.Failure(FileImportErrors.InvalidInputPath);
            }

            await PerformImportAsync(context, cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while importing orders from {@OutputPath}.", context.InputPath);
            return Result.Failure(new("", $"An error occurred while importing orders from {context.InputPath}."));
        }
    }

    private async Task PerformImportAsync(FileImportContext context, CancellationToken cancellationToken = default)
    {
        using var stream = new FileStream(context.InputPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        List<Order> orders = [];
        await foreach (var order in csv.GetRecordsAsync<Order>(cancellationToken))
        {
            orders.Add(order);
        }
        if (orders.Count == 0)
        {
            logger.LogInformation("No order to import.");
            return;
        }
        await orderService.InsertManyAsync(orders);
    }
}
