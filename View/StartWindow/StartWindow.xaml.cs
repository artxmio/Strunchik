using Strunchik.ViewModel.StartWindowViewModel;
using System.Windows;
using System.Windows.Controls;

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
