using Strunchik.ViewModel.MainWindowViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Strunchik.View.MainWindow.Pages;

public partial class ProfilePage : Page
{
    private readonly MainWindowViewModel _viewModel;

    public ProfilePage(MainWindowViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;

        this.DataContext = _viewModel;
    }

    private void ChangeNameButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel viewModel)
        {
            viewModel.NameTextboxIsReadOnly = false;
            UserNameTextBox.Focus();
            UserNameTextBox.CaretIndex = UserNameTextBox.Text.Length;
        }
    }

    private void ChangeEmailButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel viewModel)
        {
            viewModel.EmailTextboxIsReadOnly = false;
            EmailTextBox.Focus();
            EmailTextBox.CaretIndex = EmailTextBox.Text.Length;
        }
    }
    private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel viewModel)
        {
            viewModel.PasswordTextboxIsReadOnly = false;
            PasswordTextBox.Focus();
            PasswordTextBox.CaretIndex = PasswordTextBox.Text.Length;
        }
    }

    private void UserNameTextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        _viewModel.NameTextboxIsReadOnly = true;
    }

    private void EmailNameTextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        _viewModel.EmailTextboxIsReadOnly = true;
    }

    private void PasswordNameTextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        _viewModel.PasswordTextboxIsReadOnly = true;
    }
}
