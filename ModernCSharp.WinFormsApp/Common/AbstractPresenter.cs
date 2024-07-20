namespace ModernCSharp.WinFormsApp.Common;

public abstract class AbstractPresenter<TView, TPresenter>
    : IPresenter<TView, TPresenter>
     where TView : IView<TPresenter>
    where TPresenter : class, IPresenter<TView, TPresenter>
{
    protected TView _view;
    public TView View
    {
        get => _view;
        set
        {
            _view = value;
            _view.Presenter = this as TPresenter;
        }
    }
}
