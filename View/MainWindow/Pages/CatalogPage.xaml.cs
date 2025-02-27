using Strunchik.ViewModel.MainWindowViewModel;
using System.Windows.Controls;

namespace Strunchik.View.MainWindow.Pages;

public partial class CatalogPage : Page
{
    private MainWindowViewModel _viewModel;

    public CatalogPage(MainWindowViewModel viewModel)
    {
        InitializeComponent();
        
        _viewModel = viewModel;

        this.DataContext = _viewModel;
    }

    private void SearchBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == System.Windows.Input.Key.Enter)
        {
            _viewModel.SearchString = searchBox.Text;
            _viewModel.OnEnterDown();
        }
    }
}
