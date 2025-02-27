using Strunchik.View.MainWindow.Pages;
using Strunchik.ViewModel.MainWindowViewModel;
using System.Windows;

namespace Strunchik.View.MainWindow;

public partial class MainWindow : Window
{
    private readonly MainWindowViewModel _viewModel = new MainWindowViewModel();

    public MainWindow()
    {
        InitializeComponent();

        DataContext = _viewModel;
        var page = new CatalogPage(_viewModel);
        MainFrame.Navigate(page);
    }

    private void TitleBar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var viewModel = (MainWindowViewModel)DataContext;
        viewModel.DragWindowCommand.Execute(this);
    }

    private void Profile_Click(object sender, RoutedEventArgs e)
    {
        var page = new ProfilePage(_viewModel);
        MainFrame.Navigate(page);
    }
}
