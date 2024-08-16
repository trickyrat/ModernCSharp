using Microsoft.Extensions.DependencyInjection;

using ModernCSharp.Application.Abstractions;
using ModernCSharp.Application.Services.FileExport;
using ModernCSharp.Application.Services.FileExport.Handlers;
using ModernCSharp.Application.Services.FileExtraction;
using ModernCSharp.Application.Services.FileExtraction.Handlers;

namespace ModernCSharp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddFileExporters(this IServiceCollection services)
    {
        services.AddSingleton<IFileExporter, OrderJsonExporter>();
        services.AddSingleton<IFileExporter, OrderCsvExporter>();
        services.AddSingleton<IFileExporter, OrderExcelExporter>();


        services.AddSingleton(serviceProvider =>
        {
            var strategies = serviceProvider.GetServices<IFileExporter>();
            return new FileExporterFactory(strategies);
        });
        return services;
    }

    public static IServiceCollection AddFileImporters(this IServiceCollection services)
    {
        services.AddSingleton<IFileImporter, OrderJsonImporter>();
        services.AddSingleton<IFileImporter, OrderCsvImporter>();
        services.AddSingleton<IFileImporter, OrderExcelImporter>();


        services.AddSingleton(serviceProvider =>
        {
            var importers = serviceProvider.GetServices<IFileImporter>();
            return new FileImporterFactory(importers);
        });
        return services;
    }
}
