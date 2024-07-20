using ClosedXML.Excel;

using ModernCSharp.Application.Abstractions;
using ModernCSharp.Application.Enums;
using ModernCSharp.Application.Models;
using ModernCSharp.Application.Models.Errors;
using Microsoft.Extensions.Logging;
using ModernCSharp.Application.Utils;

namespace ModernCSharp.Application.Services.FileImportServices;

public class OrderExcelImportService(ILogger<OrderExcelImportService> logger, OrderService orderService) : IFileImportService
{
    public FileType FileType => FileType.EXCEL;

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
