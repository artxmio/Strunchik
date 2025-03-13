using Microsoft.EntityFrameworkCore;
using Strunchik.Model.Item;
using System.Collections.ObjectModel;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;

namespace Strunchik.ViewModel.Services.SortService;

public static class SortService
{
    public static ObservableCollection<ItemModel> SortByDescending(ObservableCollection<ItemModel> items)
    {
        return [.. items.OrderBy(i => i.Price)];
    }

    public static ObservableCollection<ItemModel> SortByAscending(ObservableCollection<ItemModel> items)
    {
        return [.. items.OrderByDescending(i => i.Price)];
    }
}
