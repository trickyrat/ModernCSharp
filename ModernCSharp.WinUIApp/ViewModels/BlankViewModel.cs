using CommunityToolkit.Mvvm.ComponentModel;

namespace ModernCSharp.WinUIApp.ViewModels;

public partial class BlankViewModel : ObservableRecipient
{
    public BlankViewModel()
    {
    }

    public async IAsyncEnumerable<int> ReportProgress()
    {
        var count = 0;
        var target = 100;
        var interval = 1;
        var delay = 100;
        while (count < target)
        {
            count += interval;
            await Task.Delay(delay);
            yield return count;
        }
    }
}
