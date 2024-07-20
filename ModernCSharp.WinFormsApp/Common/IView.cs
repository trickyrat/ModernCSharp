namespace ModernCSharp.WinFormsApp.Common;

public interface IView<TPresenter>
{
    TPresenter Presenter { get; set; }
}
