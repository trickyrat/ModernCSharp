using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.UI.Xaml.Controls;

using ModernCSharp.WinUIApp.Contracts.ViewModels;
using ModernCSharp.WinUIApp.Controls;
using ModernCSharp.WinUIApp.Core.Contracts.Services;
using ModernCSharp.WinUIApp.Core.Models;

namespace ModernCSharp.WinUIApp.ViewModels;

public partial class DataGridViewModel : ObservableRecipient, INavigationAware
{
    private readonly ISampleDataService _sampleDataService;

    public ObservableCollection<SampleOrder> Source { get; } = new ObservableCollection<SampleOrder>();

    public IAsyncRelayCommand AddOrderCommand
    {
        get;
    }


    public DataGridViewModel(ISampleDataService sampleDataService)
    {
        _sampleDataService = sampleDataService;
        AddOrderCommand = new AsyncRelayCommand(AddOrder);
    }

    public async void OnNavigatedTo(object parameter)
    {
        Source.Clear();

        // TODO: Replace with real data.
        var data = await _sampleDataService.GetGridDataAsync();

        foreach (var item in data)
        {
            Source.Add(item);
        }
    }

    public void OnNavigatedFrom()
    {
    }

    private async Task AddOrder()
    {
        var dialog = new ContentDialog 
        {
            Title = "Add Order",
            Content = new AddOrderDialog(),
            PrimaryButtonText = "Save",
            CloseButtonText = "Cancel",
        };

        var result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            var addOrderDialog = (AddOrderDialog)dialog.Content;
            var newOrder = new SampleOrder 
            {
                Company = addOrderDialog.Company,
                Status = addOrderDialog.OrderStatus
            };
            await _sampleDataService.AddAsync(newOrder);
        }
    }
}
