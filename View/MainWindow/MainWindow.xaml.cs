using Strunchik.View.MainWindow.Pages;
using Strunchik.ViewModel.MainWindowViewModel;
using System.Windows;

namespace Strunchik.View.MainWindow;

public partial class MainWindow : Window
{
    public MainWindowViewModel _viewModel;

    public MainWindow()
    {
        InitializeComponent();
        _viewModel = new MainWindowViewModel();
        DataContext = _viewModel;
        var page = new CatalogPage(_viewModel);
        MainFrame.Navigate(page);
    }

    private void TitleBar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var viewModel = (MainWindowViewModel)DataContext;
        if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            viewModel.DragWindowCommand.Execute(this);
    }

    private void OpenProfilePage(object sender, RoutedEventArgs e)
    {
        var page = new ProfilePage(_viewModel);
        MainFrame.Navigate(page);
    }

    private void OpenCatalogPage(object sender, RoutedEventArgs e)
    {
        var page = new CatalogPage(_viewModel);
        MainFrame.Navigate(page);
    }

    private void OpenBasketPage(object sender, RoutedEventArgs e)
    {
        var page = new BasketPage(_viewModel);
        MainFrame.Navigate(page);
    }

    private void OpenAboutPage(object sender, RoutedEventArgs e)
    {
        var page = new AboutPage();
        MainFrame.Navigate(page);
    }
}
