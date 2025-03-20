using Strunchik.ViewModel.MainWindowViewModel;
using System.Windows.Controls;

namespace Strunchik.View.MainWindow.Pages;

public partial class PurchaseHistoryPage : Page
{
    public PurchaseHistoryPage(MainWindowViewModel viewModel)
    {
        InitializeComponent();

        this.DataContext = viewModel;
    }
}
