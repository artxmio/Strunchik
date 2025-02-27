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
        var viewModel = DataContext as MainWindowViewModel;
        if (viewModel != null)
        {
            viewModel.ChangeNameCommand.Execute(null);
            UserNameTextBox.Focus();
            UserNameTextBox.CaretIndex = UserNameTextBox.Text.Length; // Устанавливает курсор в конец текста
        }
    }

    private void TextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            var viewModel = DataContext as MainWindowViewModel;
            viewModel.ApllyNameCommand.Execute(null);
        }
    }

}
