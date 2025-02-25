namespace Strunchik.Model.Item;

public static class ItemExtension
{
    public static string ToMyString(this ItemsType type)
    {
        return type switch
        {
            ItemsType.StringInstrument => "Струнные инструменты",
            ItemsType.KeyboardInstruments => "Клавишные инструменты",
            ItemsType.WindInsrtuments => "Духовые инструменты",
            ItemsType.PercussionInstruments => "Ударные инструменты",
            _ => "",
        };
    }
}