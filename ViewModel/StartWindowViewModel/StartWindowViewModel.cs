using Strunchik.Model.User;
using Strunchik.ViewModel.Commands;
using System.Windows;
using System.Windows.Input;

namespace Strunchik.ViewModel.StartWindowViewModel;

public class StartWindowViewModel
{
    private ApplicationContext.ApplicationContext _context;

    public UserModel NewUser { get; set; }

    public ICommand DragWindowCommand { get; }
    public ICommand CloseWindowCommand { get; }
    public ICommand RegistrationCommand { get; }

    public StartWindowViewModel()
    {
        _context = new ApplicationContext.ApplicationContext();
        NewUser = new UserModel();

        DragWindowCommand = new RelayCommand(_ => DragWindow(_));
        CloseWindowCommand = new RelayCommand(_ => CloseWindow(_));
        RegistrationCommand = new RelayCommand(_ => Registration());
    }

    private void Registration()
    {

    }

    private static void CloseWindow(object _)
    {
        if (_ is Window window)
        {
            window.Close();
        }
        else
        {
            throw new ArgumentException("Parameter was not a window");
        }
    }
    private static void DragWindow(object _)
    {
        if (_ is Window window)
        {
            window.DragMove();
        }
        else
        {
            throw new ArgumentException("Parameter was not a window");
        }
    }
}
