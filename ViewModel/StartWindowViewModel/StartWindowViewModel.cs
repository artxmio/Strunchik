using Strunchik.ViewModel.Commands;
using System.Windows;
using System.Windows.Input;

namespace Strunchik.ViewModel.StartWindowViewModel;

public class StartWindowViewModel
{
    public ICommand DragWindowCommand { get; set; }
    public ICommand CloseWindowCommand { get; }

    public StartWindowViewModel()
    {
        DragWindowCommand = new RelayCommand(_ => DragWindow(_));
        CloseWindowCommand = new RelayCommand(_ => CloseWindow(_));
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
