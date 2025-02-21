using Strunchik.ViewModel.StartWindowViewModel;
using System.Windows;

namespace Strunchik.View.StartWindow;

public partial class StartWindow : Window
{
    public StartWindow()
    {
        InitializeComponent();

        this.DataContext = new StartWindowViewModel();
    }

    private void TitleBar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var viewModel = (StartWindowViewModel)DataContext;
        viewModel.DragWindowCommand.Execute(this);
    }
}
