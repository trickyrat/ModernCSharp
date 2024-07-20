using ModernCSharp.Application.Abstractions;
using ModernCSharp.Application.Constants;
using ModernCSharp.Application.Enums;
using ModernCSharp.Application.Models;
using ModernCSharp.Application.Models.Errors;
using ModernCSharp.Application.Utils;

using Microsoft.Extensions.Logging;

using System.Text.Json;

namespace ModernCSharp.Application.Services.FileImportServices;

public class OrderJsonImportService(ILogger<OrderJsonImportService> logger, OrderService orderService) : IFileImportService
{
    public FileType FileType => FileType.JSON;

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
        var orderWrapper = await JsonSerializer.DeserializeAsync<JsonWrapper<Order>>(stream, JsonSerializationConstants.DefaultOptions, cancellationToken);

        if (orderWrapper is null || orderWrapper.Items.Count == 0)
        {
            logger.LogInformation("No order to import.");
            return;
        }
        await orderService.InsertManyAsync(orderWrapper.Items);
    }
}
