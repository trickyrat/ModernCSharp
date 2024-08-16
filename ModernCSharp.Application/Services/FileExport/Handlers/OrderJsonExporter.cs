﻿using Microsoft.Extensions.Logging;

using ModernCSharp.Application.Abstractions;
using ModernCSharp.Application.Constants;
using ModernCSharp.Application.Enums;
using ModernCSharp.Application.Models;
using ModernCSharp.Application.Models.Errors;
using ModernCSharp.Application.Models.FileExport;
using ModernCSharp.Application.Services.FileExport.Commands;
using ModernCSharp.Application.Utils;

using System.Text.Json;

namespace ModernCSharp.Application.Services.FileExport.Handlers;

public class OrderJsonExporter(ILogger<OrderJsonExporter> logger, OrderService orderService) : IFileExporter
{
    private OrderJsonExportContext _context;
    public List<FileType> FileTypes => [FileType.JSON];

    public void InitializeContext(IFileExportContext context)
    {
        if (context is OrderJsonExportContext exportContext)
        {
            _context = exportContext;
        }
        else
        {
            throw new ArgumentException("Invalid file export context");
        }
    }
    public async Task<IResult> ExportAsync(CancellationToken cancellationToken = default)
    {
        if (!FileIOUtils.ValidateExportContext<OrderJsonExportContext>(_context))
        {
            logger.LogWarning("Invalid context provided.");
            return Result.Failure(FileExportErrors.InvalidContext);
        }
        try
        {
            if (!FileIOUtils.ValidatePath(_context.OutputPath))
            {
                return Result.Failure(FileExportErrors.InvalidOutputPath);
            }
            cancellationToken.ThrowIfCancellationRequested();
            await PerformExportAsync(_context, cancellationToken);
            return Result.Success();
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Export {FileType} operation  was cancelled.", _context.FileType);
            return Result.Failure(new("", $"Export {_context.FileType} operation  was cancelled."));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while exporting orders to {@OutputPath}.", _context.OutputPath);
            return Result.Failure(new("", $"An error occurred while generating order json file."));
        }
    }
    private async Task PerformExportAsync(OrderJsonExportContext context, CancellationToken cancellationToken = default)
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