using Strunchik.Model.User;
using Strunchik.ViewModel.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Strunchik.ViewModel.StartWindowViewModel;

public class StartWindowViewModel : INotifyPropertyChanged
{
    private readonly ApplicationContext.ApplicationContext _context;

    public event PropertyChangedEventHandler? PropertyChanged;

    public UserModel NewUser { get; set; }
    public UserModel AuthUser { get; set; }

    public ICommand DragWindowCommand { get; }
    public ICommand CloseWindowCommand { get; }
    public ICommand RegistrationCommand { get; }

    private string _errorMessage = "";
    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            if (_errorMessage != value)
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public StartWindowViewModel()
    {
        _context = new ApplicationContext.ApplicationContext();
        NewUser = new UserModel();
        AuthUser = new UserModel();

        DragWindowCommand = new RelayCommand(_ => DragWindow(_));
        CloseWindowCommand = new RelayCommand(_ => CloseWindow(_));
        RegistrationCommand = new RelayCommand(_ => Registration());
    }

    private void Registration()
    {
        Regex EmailRegex = new Regex("^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$", RegexOptions.Compiled);
        Regex NameRegex = new Regex("^[A-Za-zА-Яа-яЁё\\s]+$", RegexOptions.Compiled);
        Regex PasswordRegex = new Regex("^[A-Za-z\\d@$!%*?&]{8,}$", RegexOptions.Compiled);

        var b2 = NewUser.Email is null || NewUser.Password is null || NewUser.Name is null;
        
        if (b2)
        {
            ErrorMessage = "Введите все обязательные поля";
        }
        else
        {
            ErrorMessage = "Успех";
        }
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
