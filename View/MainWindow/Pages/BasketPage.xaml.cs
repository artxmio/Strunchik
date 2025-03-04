using Strunchik.ViewModel.MainWindowViewModel;
using System.Windows.Controls;

namespace Strunchik.View.MainWindow.Pages;

public partial class BasketPage : Page
{
    private readonly MainWindowViewModel _viewModel;

    public BasketPage(MainWindowViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;

        this.DataContext = _viewModel;
    }
}
