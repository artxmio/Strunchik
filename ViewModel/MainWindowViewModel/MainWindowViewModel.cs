using Microsoft.EntityFrameworkCore;
using Strunchik.Model.Basket;
using Strunchik.Model.CartItem;
using Strunchik.Model.Item;
using Strunchik.Model.User;
using Strunchik.View.StartWindow;
using Strunchik.ViewModel.Commands;
using Strunchik.ViewModel.Services.BasketService;
using Strunchik.ViewModel.Services.ProfileTextBoxsService;
using Strunchik.ViewModel.Services.SearchService;
using Strunchik.ViewModel.Services.UserSaveService;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Strunchik.ViewModel.MainWindowViewModel;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly ApplicationContext.ApplicationContext _context;
    private ItemModel _selectedItem = null!;
    private GridLength _selectedWidth = new(0);
    private int _quantity = 1;

    private readonly SearchService _searchService;
    private readonly ProfileTextBoxsService _profileTextBoxsService;
    private readonly UserSaveService _userSaveService;
    private readonly BasketService _basketService;
    private BasketModel _basket;

    private bool _isUserNotAuthorizate = true;
    private UserModel _currentUser = new();

    public ItemModel SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            OnPropertyChanged();
            OnItemSelected(new GridLength(1, GridUnitType.Star));
        }
    }
    public ObservableCollection<ItemModel> Items { get; set; }

    public UserModel CurrentUser
    {
        get => _currentUser;
        set
        {
            _currentUser = value;
            OnPropertyChanged();
        }
    }

    public BasketModel Basket
    {
        get => _basket;
        set
        {
            _basket = value;
            OnPropertyChanged(nameof(Basket));
            OnPropertyChanged(nameof(CartItems));
        }
    }
    public ObservableCollection<CartItemModel> CartItems => [.. Basket?.CartItems ?? []];

    // Profile TextBoxs ReadOnly States
    public bool NameTextboxIsReadOnly
    {
        get => _profileTextBoxsService.GetTextBoxState("name");
        set
        {
            _profileTextBoxsService.SetTextBoxState("name", value);
            OnPropertyChanged();
        }
    }
    public bool EmailTextboxIsReadOnly
    {
        get => _profileTextBoxsService.GetTextBoxState("email");
        set
        {
            _profileTextBoxsService.SetTextBoxState("email", value);
            OnPropertyChanged();
        }
    }
    public bool PasswordTextboxIsReadOnly
    {
        get => _profileTextBoxsService.GetTextBoxState("password");
        set
        {
            _profileTextBoxsService.SetTextBoxState("password", value);
            OnPropertyChanged();
        }
    }

    // Commands
    #region
    public ICommand DragWindowCommand { get; }
    public ICommand RestoreWindowCommand { get; }
    public ICommand RollWindowCommand { get; }
    public ICommand CloseWindowCommand { get; }
    public ICommand OpenAuthorizationRegistrationCommand { get; }
    public ICommand CloseItemDescriptionCommand { get; }
    public ICommand ExitCommand { get; }
    public ICommand AddItemToBasketCommand { get; }

    public ICommand SaveCommand { get; }
    public ICommand DeleteAccountCommand { get; }

    public ICommand DecreaseQuantityCommand { get; }
    public ICommand IncreaseQuantityCommand { get; }
    #endregion

    public decimal TotalPrice 
    {
        get
        {
            return Basket.CartItems?.Sum(ci => ci.Item.Price * ci.Quantity) ?? 0;
        }
    }

    public GridLength SelectedWidth
    {
        get => _selectedWidth;
        set
        {
            if (_selectedWidth != value)
            {
                _selectedWidth = value;
            }

            OnPropertyChanged();
        }
    }
    public string SearchString
    {
        get => _searchService.SearchString;
        set
        {
            _searchService.SearchString = value;
            OnPropertyChanged();
        }
    }

    public bool IsUserNotAuthorizate
    {
        get => _isUserNotAuthorizate;
        set
        {
            _isUserNotAuthorizate = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsUserAuthorizate));
        }
    }
    public bool IsUserAuthorizate => !_isUserNotAuthorizate;

    public int Quantity
    {
        get => _quantity;
        set
        {
            _quantity = value;
            OnPropertyChanged(nameof(Quantity));
        }
    }

    public MainWindowViewModel()
    {
        _context = new ApplicationContext.ApplicationContext();
        _userSaveService = new UserSaveService();
        _searchService = new SearchService();
        _profileTextBoxsService = new ProfileTextBoxsService();
        _basketService = new BasketService(_context);

        _context.Database.EnsureCreated();
        _context.Items.Load();
        _context.Users.Load();
        _context.Baskets.Load();
        _context.CartItems.Load();
        _context.InstrumentTypes.Load();
        Items = _context.Items.Local.ToObservableCollection();

        if (_userSaveService.User is not null)
        {
            var curr = _context.Users.Local.FirstOrDefault(user => user.Email == _userSaveService.User.Email);

            CurrentUser = curr ?? new UserModel();
            _isUserNotAuthorizate = false;
        }
        _basket = _context.Baskets.Local.FirstOrDefault(b => b.UserId == CurrentUser.UserId)
                    ?? new BasketModel();

        DragWindowCommand = new RelayCommand(_ => DragWindow(_));
        RestoreWindowCommand = new RelayCommand(_ => RestoreWindow(_));
        RollWindowCommand = new RelayCommand(_ => RollWindow(_));
        CloseWindowCommand = new RelayCommand(_ => CloseWindow(_));
        CloseItemDescriptionCommand = new RelayCommand(_ => CloseItemDescription());
        OpenAuthorizationRegistrationCommand = new RelayCommand(_ => OpenAuthWindow());
        ExitCommand = new RelayCommand(_ => Exit());
        DeleteAccountCommand = new RelayCommand(_ => DeleteAccount());
        AddItemToBasketCommand = new RelayCommand(_ => _basketService.AddItemToBasket(CurrentUser.UserId, SelectedItem.ItemId, Quantity));

        IncreaseQuantityCommand = new RelayCommand(_ => Quantity++);
        DecreaseQuantityCommand = new RelayCommand(_ => { if (Quantity > 1) Quantity--; });

        SaveCommand = new RelayCommand(_ => Save());
    }

    private void DeleteAccount()
    {
        if (CurrentUser is not null)
        {
            var result = MessageBox.Show("Вы уверены, что хотите удалить свой аккаунт?", "Внимание", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                _context.Users.Remove(CurrentUser);
                CurrentUser.Name = "";
                CurrentUser.Password = "";
                CurrentUser.Email = "";
                OnPropertyChanged(nameof(CurrentUser));
                _context.SaveChanges();
                MessageBox.Show("Аккаунт удалён", "Успех");
            }
        }
    }

    private void Save()
    {
        var user = _context.Users.Local.SingleOrDefault(u => u.UserId == CurrentUser.UserId);

        if (user is not null)
        {
            _context.SaveChanges();
            _userSaveService.SaveUserData(CurrentUser);
        }
    }

    private void OpenAuthWindow()
    {
        var authRegViewModel = new StartWindowViewModel.StartWindowViewModel();

        var authRegWindow = new StartWindow(authRegViewModel);

        authRegWindow.ShowDialog();
        var result = authRegWindow.DialogResult;

        if (result is not null)
        {
            IsUserNotAuthorizate = !(bool)result;
            if ((bool)result)
            {
                _context.Users.Load();
                CurrentUser = _context.Users.Local.First(user => authRegViewModel.AuthUser.Email == user.Email);
            }

            OnPropertyChanged(nameof(IsUserAuthorizate));
        }
        else
        {
            throw new NullReferenceException();
        }
    }
    private void Exit()
    {
        _userSaveService.DeleteUserData();
        CurrentUser = null;
        IsUserNotAuthorizate = true;
    }
    private void CloseItemDescription()
    {
        _selectedItem = null!;
        OnPropertyChanged(nameof(SelectedItem));
        OnItemSelected(new GridLength(0));
    }

    // search items by _searchString
    public void SearchItems()
    {
        Items = string.IsNullOrEmpty(SearchString) ? _context.Items.Local.ToObservableCollection() : _searchService.Find(Items);
        OnPropertyChanged(nameof(Items));
    }

    //event processing
    public event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler<ItemModel>? ItemSelected;

    public void SetFilterOption(ItemsType type = null)
    {
        if (type == null)
        {
            Items = _context.Items.Local.ToObservableCollection();
            return;
        }
        Items = [.. _context.Items.Local.Where(i => i.ItemType.Id == type.Id)];
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null!)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    protected virtual void OnItemSelected(GridLength width)
    {
        SelectedWidth = width;
        ItemSelected?.Invoke(this, SelectedItem);
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
            if (window.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Normal;
            }
            window.DragMove();
        }
        else
        {
            throw new ArgumentException("Parameter was not a window");
        }
    }
    private static void RestoreWindow(object _)
    {
        if (_ is Window window)
        {
            window.WindowState = window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }
    }
    private static void RollWindow(object _)
    {
        if (_ is Window window)
        {
            window.WindowState = WindowState.Minimized;
        }
    }
}
