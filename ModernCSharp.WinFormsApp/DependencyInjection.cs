using Microsoft.Extensions.DependencyInjection;

using ModernCSharp.WinFormsApp.Presenters;
using ModernCSharp.WinFormsApp.Views;

namespace ModernCSharp.WinFormsApp;

public static class DependencyInjection
{
    public static IServiceCollection AddForms(this IServiceCollection services)
    {
        services.AddSingleton<MainForm>();

        services.AddTransient<ImportOrdersView>();
        services.AddTransient<OrderListView>();


        services.AddTransient<ImportOrderPresenter>();
        services.AddTransient<OrdersListPresenter>();
        return services;
    }
}
