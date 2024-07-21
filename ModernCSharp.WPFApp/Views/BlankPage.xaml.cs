using System.Windows.Controls;

using Microsoft.Win32;

using ModernCSharp.WPFApp.ViewModels;

namespace ModernCSharp.WPFApp.Views;

public partial class BlankPage : Page
{
    public BlankPage(BlankViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private void OpenFileDialogButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog();

        var result = openFileDialog.ShowDialog();

        if (result == true)
        {

        }
    }
}
