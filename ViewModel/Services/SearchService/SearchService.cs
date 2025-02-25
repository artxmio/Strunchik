using Strunchik.Model.Item;
using System.Collections.ObjectModel;

namespace Strunchik.ViewModel.Services.SearchService;

public static class SearchService
{
    //return a collection with all items matching str
    public static ObservableCollection<ItemModel> Find(ObservableCollection<ItemModel> collection, string str)
    {
        if (string.IsNullOrEmpty(str)) 
            return collection;

        return new(collection.Where(obj => obj.Title.Contains(str, StringComparison.OrdinalIgnoreCase)));
    }
}
