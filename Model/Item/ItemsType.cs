using System.ComponentModel.DataAnnotations;

namespace Strunchik.Model.Item;

public class ItemsType
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; }

    //StringInstrument = 1,
    //KeyboardInstruments,
    //WindInsrtuments,
    //PercussionInstruments
}
