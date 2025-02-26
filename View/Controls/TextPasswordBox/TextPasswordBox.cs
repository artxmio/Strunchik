using System.Windows.Controls;
using System.Windows;

namespace Strunchik.View.Controls.TextPasswordBox;

public class TextPasswordBox : TextBox
{
    public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.Register("Password", typeof(string), typeof(TextPasswordBox), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPasswordPropertyChanged));

    public static readonly DependencyProperty IsPasswordVisibleProperty =
        DependencyProperty.Register("IsPasswordVisible", typeof(bool), typeof(TextPasswordBox), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsPasswordVisiblePropertyChanged));


    public const char CHAR = '●';

    public TextPasswordBox()
    {
        Text = new string(CHAR, Password.Length);
    }

    public string Password
    {
        get
        {
            return (string)GetValue(PasswordProperty);
        }
        set
        {
            SetValue(PasswordProperty, value);
        }
    }

    public bool IsPasswordVisible
    {
        get
        {
            return (bool)GetValue(IsPasswordVisibleProperty);
        }
        set
        {
            if (Text != Password)
            {
                Text = Password;
            }
            else
            {
                Text = new string(CHAR, Password.Length);
            }
            SetValue(IsPasswordVisibleProperty, value);
        }
    }

    private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TextPasswordBox passwordBox)
        {
            passwordBox.UpdateText();
        }
    }

    private static void OnIsPasswordVisiblePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TextPasswordBox passwordBox)
        {
            passwordBox.UpdateText();
        }
    }

    protected override void OnTextChanged(TextChangedEventArgs e)
    {
        var diff = Text.Length - Password.Length;

        if (diff == 0)
        {
            return;
        }

        if (diff < 0)
        {
            var charsToRemove = Password.Length - Text.Length;

            var startIndex = CaretIndex;

            Password = Password.Remove(startIndex, charsToRemove);

            this.Text = new string(CHAR, Password.Length);

            CaretIndex = Text.Length;

            return;
        }
        else
        {
            if (!IsPasswordVisible)
            {
                var newPasswordChars = new string((from ch in Text where ch != CHAR select ch).ToArray());

                if (Password.Length != Text.Length)
                {
                    Password = new string(Password + newPasswordChars);
                }

                this.Text = new string(CHAR, Password.Length);
            }
            else
            {
                var newPasswordChars = new string((from ch in Text where ch != CHAR select ch).ToArray());

                Password = new string(newPasswordChars);

                this.Text = Password;
            }
            CaretIndex = Text.Length;
            return;
        }
    }

    private void UpdateText()
    {
        if (IsPasswordVisible)
        {
            Text = Password;
        }
        else
        {
            Text = new string(CHAR, Password.Length);
        }
    }
}
