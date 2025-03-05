namespace Strunchik.ViewModel.Services.ProfileTextBoxsService;

public class ProfileTextBoxsService
{
    private Dictionary<string, bool> _textBoxStates = new()
    {
        {"email", true },
        { "name", true },
        {"password", true }
    };

    public bool GetTextBoxState(string textBoxName)
    {
        if (!_textBoxStates.TryGetValue(textBoxName, out _))
        {
            throw new ArgumentException("Does not have this state");
        }

        return _textBoxStates[textBoxName];
    }

    public bool SetTextBoxState(string textBoxName, bool value)
    {
        if (!_textBoxStates.TryGetValue(textBoxName, out _))
        {
            throw new ArgumentException("Does not have this state");
        }

        _textBoxStates[textBoxName] = value;
        return true;
    }
}
