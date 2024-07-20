using ClosedXML.Excel;
using Microsoft.Extensions.Logging;
using ModernCSharp.Application.Abstractions;
using ModernCSharp.Application.Enums;
using ModernCSharp.Application.Models;
using ModernCSharp.Application.Models.Errors;
using ModernCSharp.Application.Utils;

namespace ModernCSharp.Application.Services.FileExportServices;

public class OrderExcelExportService(ILogger<OrderExcelExportService> logger, OrderService orderService) : IFileExportService
{
    public FileType FileType => FileType.EXCEL;

    public async Task<IResult> ExportAsync(FileExportContext context, CancellationToken cancellationToken = default)
    {
        if (!FilePathUtils.ValidateExportContext(context))
        {
            logger.LogWarning("Invalid export context provided.");
            return Result.Failure(FileImportErrors.InvalidContext);
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

        using var wb = new XLWorkbook();
        var ws = wb.AddWorksheet("Worksheet 1");

        var headerRow = ws.Row(1);
        var properties = typeof(Order).GetProperties();

        int headerCellIndex = 1;
        foreach (var property in properties)
        {
            headerRow.Cell(headerCellIndex).SetValue(property.Name);
            headerCellIndex++;
        }

        int contentRowIndex = 2;
        foreach (var order in orders)
        {
            var currentRow = ws.Row(contentRowIndex);
            currentRow.Cell("A").SetValue(order.Id);
            currentRow.Cell("B").SetValue(order.OrderStatus.ToString());
            currentRow.Cell("C").SetValue(order.Quantity);
            currentRow.Cell("D").SetValue(order.Total);

            contentRowIndex++;
        }

        wb.SaveAs(context.OutputPath);
    }
}
