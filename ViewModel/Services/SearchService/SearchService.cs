using Strunchik.Model.Item;
using System.Collections.ObjectModel;

namespace Strunchik.ViewModel.Services.SearchService;

public class SearchService
{
    private string _searchString = "";

    public string SearchString
    {
        get => _searchString; 
        set => _searchString = value;
    }

    //return a collection with all items matching str
    public ObservableCollection<ItemModel> Find(ObservableCollection<ItemModel> collection)
    {
        if (string.IsNullOrEmpty(_searchString)) 
            return collection;

        return [.. collection.Where(obj => obj.Title.Contains(_searchString, StringComparison.OrdinalIgnoreCase))];
    }
}
