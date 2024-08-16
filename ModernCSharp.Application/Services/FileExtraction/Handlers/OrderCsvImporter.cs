using System.Globalization;

using CsvHelper;

using Microsoft.Extensions.Logging;

using ModernCSharp.Application.Abstractions;
using ModernCSharp.Application.Enums;
using ModernCSharp.Application.Models;
using ModernCSharp.Application.Models.Errors;
using ModernCSharp.Application.Models.FileExtraction;
using ModernCSharp.Application.Services.FileExtraction.Commands;
using ModernCSharp.Application.Utils;

namespace ModernCSharp.Application.Services.FileExtraction.Handlers;

public class OrderCsvImporter(ILogger<OrderCsvImporter> logger, OrderService orderService) : IFileImporter
{
    private OrderCsvImportContext _context;
    public List<FileType> FileTypes => [FileType.CSV];

    public void InitializeContext(IFileImportContext context)
    {
        if (context is not OrderCsvImportContext importContext)
        {
            logger.LogError("Invalid file import context type.");
            throw new ArgumentException("Invalid file import context");
        }
        _context = importContext;
    }

    public async Task<IResult> ImportAsync(CancellationToken cancellationToken = default)
    {
        if (!FileIOUtils.ValidateImportContext<OrderCsvImportContext>(_context))
        {
            logger.LogWarning("Invalid import context provided.");
            return Result.Failure(FileImportErrors.InvalidContext);
        }

        try
        {
            if (!FileIOUtils.ValidatePath(_context.InputPath))
            {
                return Result.Failure(FileImportErrors.InvalidInputPath);
            }
            cancellationToken.ThrowIfCancellationRequested();
            await PerformImportAsync(_context, cancellationToken);
            return Result.Success();
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Import {FileType} operation  was cancelled.", _context.FileType);
            return Result.Failure(new("", $"Import {_context.FileType} operation  was cancelled."));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while importing orders from {@OutputPath}.", _context.InputPath);
            return Result.Failure(new("", $"An error occurred while importing orders from {_context.InputPath}."));
        }
    }

    private async Task PerformImportAsync(OrderCsvImportContext context, CancellationToken cancellationToken = default)
    {
        await using var stream = new FileStream(context.InputPath, FileMode.Open, FileAccess.Read, FileShare.Read);
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
