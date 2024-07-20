using Microsoft.Extensions.DependencyInjection;

using ModernCSharp.Application.Abstractions;
using ModernCSharp.Application.Services.FileExportServices;
using ModernCSharp.Application.Services.FileImportServices;

namespace ModernCSharp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddFileExporters(this IServiceCollection services)
    {
        services.AddSingleton<IFileExportService, OrderJsonExportService>();
        services.AddSingleton<IFileExportService, OrderCsvExportService>();
        services.AddSingleton<IFileExportService, OrderExcelExportService>();


        services.AddSingleton(serviceProvider =>
        {
            var strategies = serviceProvider.GetServices<IFileExportService>();
            return new ExportService(strategies);
        });
        return services;
    }

    public static IServiceCollection AddFileImporters(this IServiceCollection services)
    {
        services.AddSingleton<IFileImportService, OrderJsonImportService>();
        services.AddSingleton<IFileImportService, OrderCsvImportService>();
        services.AddSingleton<IFileImportService, OrderExcelImportService>();


        services.AddSingleton(serviceProvider =>
        {
            var strategies = serviceProvider.GetServices<IFileImportService>();
            return new ImportService(strategies);
        });
        return services;
    }
}
