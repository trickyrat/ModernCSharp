using Microsoft.Extensions.DependencyInjection;

namespace ModernCSharp.WinFormsApp.Views;

public partial class MainForm : Form
{
    private readonly IServiceProvider _serviceProvider;

    public MainForm()
    {
        InitializeComponent();
    }

    public MainForm(IServiceProvider serviceProvider) : this()
    {
        _serviceProvider = serviceProvider;
    }

    private void MainForm_Load(object sender, EventArgs e)
    {

    }

    private void orderListToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var orderListView = _serviceProvider.GetRequiredService<OrderListView>();

        orderListView.Show();
    }
}
