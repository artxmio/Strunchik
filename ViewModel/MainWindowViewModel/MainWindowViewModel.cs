using Microsoft.EntityFrameworkCore;
using Strunchik.Model.Item;
using Strunchik.ViewModel.Commands;
using Strunchik.ViewModel.Services.SearchService;
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
    private string _searchString = "";

    public event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler<ItemModel>? ItemSelected;

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
    public ICommand DragWindowCommand { get; }
    public ICommand RestoreWindowCommand { get; }
    public ICommand RollWindowCommand { get; }
    public ICommand CloseWindowCommand { get; }

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

    public ICommand CloseItemDescriptionCommand { get; }

    public string SearchString
    {
        get => _searchString;
        set
        {
            _searchString = value;
            OnPropertyChanged();
        }
    }

    public MainWindowViewModel()
    {
        _context = new ApplicationContext.ApplicationContext();

        _context.Database.EnsureCreated();
        _context.Items.Load();
        Items = _context.Items.Local.ToObservableCollection();

        DragWindowCommand = new RelayCommand(_ => DragWindow(_));
        RestoreWindowCommand = new RelayCommand(_ => RestoreWindow(_));
        RollWindowCommand = new RelayCommand(_ => RollWindow(_));
        CloseWindowCommand = new RelayCommand(_ => CloseWindow(_));
        CloseItemDescriptionCommand = new RelayCommand(_ => CloseItemDescription());
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
    private void CloseItemDescription()
    {
        _selectedItem = null;
        OnItemSelected(new GridLength(0));
    }

    public void OnEnterDown()
    {
        Items = string.IsNullOrEmpty(SearchString) ? _context.Items.Local.ToObservableCollection() : SearchService.Find(Items, _searchString);
        OnPropertyChanged(nameof(Items));
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
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
}
