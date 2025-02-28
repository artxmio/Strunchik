using Strunchik.ViewModel.StartWindowViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Strunchik.View.StartWindow;

public partial class StartWindow : Window
{
    private StartWindowViewModel _viewModel;

    public StartWindow(StartWindowViewModel viewMolel)
    {
        InitializeComponent();

        _viewModel = viewMolel;

        this.DataContext = viewMolel;
    }

    private void TitleBar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var viewModel = (StartWindowViewModel)DataContext;
        viewModel.DragWindowCommand.Execute(this);
    }

    private void ShowPasswordAuth(object sender, System.Windows.RoutedEventArgs e)
    {
        authPasswordTextBox.IsPasswordVisible = !authPasswordTextBox.IsPasswordVisible;

        if (sender is Button button)
        {
            button.Tag = button.Tag?.ToString() == "0" ? "1" : "0";
        }
    }

    private void ShowPasswordReg(object sender, System.Windows.RoutedEventArgs e)
    {
        regPasswordTextBox.IsPasswordVisible = !regPasswordTextBox.IsPasswordVisible;

        if (sender is Button button)
        {
            button.Tag = button.Tag?.ToString() == "0" ? "1" : "0";
        }
    }
}
