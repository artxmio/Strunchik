using Strunchik.Model.Item;
using System.Collections.ObjectModel;

namespace Strunchik.ViewModel.Services.SortService;

public static class SortService
{
    public static ObservableCollection<ItemModel> SortByPrice(ObservableCollection<ItemModel> items, bool ascending)
    {
        return ascending? [.. items.OrderBy(i => i.Price)] : [.. items.OrderByDescending(i => i.Price)];
    }

    public static ObservableCollection<ItemModel> SortByTitle(ObservableCollection<ItemModel> items, bool ascending)
    {
        return ascending ? [.. items.OrderBy(i => i.Title)] : [.. items.OrderByDescending(i => i.Title)];
    }
}
