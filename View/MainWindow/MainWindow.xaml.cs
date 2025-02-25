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
    }

    private void TitleBar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var viewModel = (MainWindowViewModel)DataContext;
        viewModel.DragWindowCommand.Execute(this);
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
