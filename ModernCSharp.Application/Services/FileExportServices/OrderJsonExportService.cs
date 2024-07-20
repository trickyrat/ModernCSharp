using Microsoft.Extensions.Logging;
using ModernCSharp.Application.Abstractions;
using ModernCSharp.Application.Constants;
using ModernCSharp.Application.Enums;
using ModernCSharp.Application.Models;
using ModernCSharp.Application.Models.Errors;
using ModernCSharp.Application.Utils;
using System.Text.Json;

namespace ModernCSharp.Application.Services.FileExportServices;

public class OrderJsonExportService(ILogger<OrderJsonExportService> logger, OrderService orderService) : IFileExportService
{
    public FileType FileType => FileType.JSON;

    public async Task<IResult> ExportAsync(FileExportContext context, CancellationToken cancellationToken = default)
    {
        if (!FilePathUtils.ValidateExportContext(context))
        {
            logger.LogWarning("Invalid context provided.");
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
            return Result.Failure(new("", $"An error occurred while generating order json file."));
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

        var orderWrapper = new JsonWrapper<Order>(orders);
        using var stream = new FileStream(context.OutputPath, FileMode.Create, FileAccess.Write, FileShare.Read);
        await JsonSerializer.SerializeAsync(stream, orderWrapper, JsonSerializationConstants.DefaultOptions, cancellationToken);
        await stream.FlushAsync(cancellationToken);
    }
}