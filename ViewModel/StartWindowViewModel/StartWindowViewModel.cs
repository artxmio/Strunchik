using Microsoft.EntityFrameworkCore;
using Strunchik.Model.User;
using Strunchik.View.MainWindow;
using Strunchik.ViewModel.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Strunchik.ViewModel.StartWindowViewModel;

public class StartWindowViewModel : INotifyPropertyChanged
{
    private readonly ApplicationContext.ApplicationContext _context;
    private string _errorRegMessage = "";
    private string _errorAuthMessage = "";
    private bool _isEnabledAuthButton = true;
    private bool _isEnabledRegButton = true;

    public event PropertyChangedEventHandler? PropertyChanged;

    public UserModel NewUser { get; set; }
    public UserModel AuthUser { get; set; }

    public ICommand DragWindowCommand { get; }
    public ICommand CloseWindowCommand { get; }
    public ICommand RegistrationCommand { get; }
    public ICommand AuthorizationCommand { get; }

    public string ErrorRegMessage
    {
        get => _errorRegMessage;
        set
        {
            if (_errorRegMessage != value)
            {
                _errorRegMessage = value;
                OnPropertyChanged();
            }
        }
    }
    public string ErrorAuthMessage
    {
        get => _errorAuthMessage;
        set
        {
            if (_errorAuthMessage != value)
            {
                _errorAuthMessage = value;
                OnPropertyChanged();
            }
        }
    }

    public bool IsEnabledAuthButton
    {
        get => _isEnabledAuthButton;
        set
        {
            _isEnabledAuthButton = value;
            OnPropertyChanged();
        }
    }
    public bool IsEnabledRegButton
    {
        get => _isEnabledRegButton;
        set
        {
            _isEnabledRegButton = value;
            OnPropertyChanged();
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
        AuthorizationCommand = new RelayCommand(_ => Authorization(_));
    }

    private void Registration()
    {
        IsEnabledRegButton = false;

        if (NewUser.Email is null || NewUser.Password is null || NewUser.Name is null)
        {
            ErrorRegMessage = "Введите все обязательные поля";
        }
        else
        {
            _context.Users.Load();
            var users = _context.Users.Local.ToList();

            bool isExist = users.Any(user => user.Email == NewUser.Email);

            if (isExist)
            {
                ErrorRegMessage = "Пользователь с такой почтой уже существует";
            }
            else
            {
                var state = _context.Users.Add(NewUser);
                _context.ChangeTracker.Clear();
                _context.Entry(NewUser).State = EntityState.Added;
                _context.SaveChanges();
                
                if(state.State == EntityState.Added)
                {
                    MessageBox.Show("Поздравляю, вы успешно зарегистрировались! Теперь вы можете авторизоваться.", "Успех", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Не удалось добавить пользователя в базу данных", "Ошибка");
                }
            }
        }
        IsEnabledRegButton = true;
    }

    private void Authorization(object _)
    {
        IsEnabledAuthButton = false;
        if (AuthUser.Email is null || AuthUser.Password is null)
        {
            ErrorAuthMessage = "Введите все обязательные поля";
        }
        else
        {
            _context.Users.Load();
            var users = _context.Users.Local.ToList();

            bool isExist = users.Any(user => user.Email == AuthUser.Email);

            if (!isExist)
            {
                ErrorAuthMessage = "Такого пользователя не существуют";
            }
            else
            {
                var mainWindow = new MainWindow();

                mainWindow.Show();

                CloseWindowCommand.Execute((Window)_);
            }
        }
        IsEnabledAuthButton = true;
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
