using Strunchik.ViewModel.MainWindowViewModel;
using System.Windows;

namespace Strunchik.View.MainWindow;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var viewModel = new MainWindowViewModel();

        DataContext = viewModel;
    }

    private void TitleBar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var viewModel = (MainWindowViewModel)DataContext;
        viewModel.DragWindowCommand.Execute(this);
    }
}
