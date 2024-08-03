using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using ModernCSharp.WinUIApp.ViewModels;

using Windows.Storage.Pickers;

namespace ModernCSharp.WinUIApp.Views;

public sealed partial class BlankPage : Page
{
    public BlankViewModel ViewModel
    {
        get;
    }

    public BlankPage()
    {
        ViewModel = App.GetService<BlankViewModel>();
        InitializeComponent();
    }

    private async void OpenOrderFileButton_Click(object sender, RoutedEventArgs e)
    {
        var orderFilePicker = new FileOpenPicker()
        {
            ViewMode = PickerViewMode.Thumbnail,
            SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
        };

        var window = App.MainWindow;
        var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
        WinRT.Interop.InitializeWithWindow.Initialize(orderFilePicker, hWnd);

        orderFilePicker.FileTypeFilter.Add(".csv");
        orderFilePicker.FileTypeFilter.Add(".json");
        orderFilePicker.FileTypeFilter.Add(".xlsx");

        var selectedOrderFile = await orderFilePicker.PickSingleFileAsync();

        if (selectedOrderFile != null)
        {
            OrderFilePathTextBlock.Text = $"Order file path: {selectedOrderFile.Name}";
        }
        else
        {
            OrderFilePathTextBlock.Text = "Operation cancelled.";
        }
    }

    private async void StartButton_Click(object sender, RoutedEventArgs e)
    {
        await foreach (var progress in ViewModel.ReportProgress())
        {
            FetchDataProgressBar.Value = progress;
            FetchDataProgressRing.Value = progress;
        }
    }
}
