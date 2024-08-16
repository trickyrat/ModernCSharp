using System.Text.Json;

using Microsoft.Extensions.Logging;

using ModernCSharp.Application.Abstractions;
using ModernCSharp.Application.Constants;
using ModernCSharp.Application.Enums;
using ModernCSharp.Application.Models;
using ModernCSharp.Application.Models.Errors;
using ModernCSharp.Application.Models.FileExtraction;
using ModernCSharp.Application.Services.FileExtraction.Commands;
using ModernCSharp.Application.Utils;

namespace ModernCSharp.Application.Services.FileExtraction.Handlers;

public class OrderJsonImporter(ILogger<OrderJsonImporter> logger, OrderService orderService) : IFileImporter
{
    private OrderJsonImportContext _context;
    public List<FileType> FileTypes => [FileType.JSON];

    public void InitializeContext(IFileImportContext context)
    {
        if (context is OrderJsonImportContext importContext)
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
        if (!FileIOUtils.ValidateImportContext<OrderJsonImportContext>(_context))
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
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while importing orders from {@OutputPath}.", _context.InputPath);
            return Result.Failure(new("", $"An error occurred while importing orders from {_context.InputPath}."));
        }
    }

    private async Task PerformImportAsync(OrderJsonImportContext context, CancellationToken cancellationToken = default)
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
