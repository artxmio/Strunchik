using System.Diagnostics;
using System.Windows.Controls;

namespace Strunchik.View.MainWindow.Pages;

public partial class ContactPage : Page
{
    public ContactPage()
    {
        InitializeComponent();
    }

    private void TG_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "https://t.me/strunchic",
            UseShellExecute = true
        });
    }

    private void INST_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "https://www.instagram.com/strunchic?igsh=Y3cza2sxNG9pbnlt",
            UseShellExecute = true
        });
    }
    private void WEB_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "https://artxmio.github.io/",
            UseShellExecute = true
        });
    }
}
