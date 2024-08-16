using ClosedXML.Excel;

using Microsoft.Extensions.Logging;

using ModernCSharp.Application.Abstractions;
using ModernCSharp.Application.Enums;
using ModernCSharp.Application.Models;
using ModernCSharp.Application.Models.Errors;
using ModernCSharp.Application.Models.FileExtraction;
using ModernCSharp.Application.Services.FileExtraction.Commands;
using ModernCSharp.Application.Utils;

namespace ModernCSharp.Application.Services.FileExtraction.Handlers;

public class OrderExcelImporter(ILogger<OrderExcelImporter> logger, OrderService orderService) : IFileImporter
{
    private OrderExcelImportContext _context;

    public List<FileType> FileTypes => [FileType.EXCEL];

    public void InitializeContext(IFileImportContext context)
    {
        if (context is OrderExcelImportContext importContext)
        {
            _context = importContext;
        }
        else
        {
            throw new ArgumentException("Invalid file import context");
        }
    }

    public async Task<IResult> ImportAsync(CancellationToken cancellationToken = default)
    {
        if (!FileIOUtils.ValidateImportContext<OrderExcelImportContext>(_context))
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
    private async Task PerformImportAsync(OrderExcelImportContext context, CancellationToken cancellationToken = default)
    {
        using var stream = new FileStream(context.InputPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        var wb = new XLWorkbook(stream);
        var ws = wb.Worksheet(1);

        List<Order> orders = [];

        foreach (var row in ws.RowsUsed())
        {
            var valueOfCellA = row.Cell("A").GetValue<string>();
            if (valueOfCellA == "Id")
            {
                continue;
            }
            var orderStatusStr = row.Cell("B").GetValue<string>();
            if (!Enum.TryParse(orderStatusStr, out OrderStatus orderStatus))
            {
                logger.LogWarning("Invalid order status found in excel file.");
                continue;
            }

            var newOrder = new Order
            {
                Id = row.Cell("A").GetValue<int>(),
                OrderStatus = orderStatus,
                Quantity = row.Cell("C").GetValue<int>(),
                Total = row.Cell("D").GetValue<int>(),
            };

            orders.Add(newOrder);
        }

        if (orders.Count == 0)
        {
            logger.LogInformation("No order to import.");
            return;
        }
        await orderService.InsertManyAsync(orders);
    }
}
