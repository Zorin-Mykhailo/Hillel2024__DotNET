using PhoneBook.Data;
using PhoneBook.Models;
using System.Collections.ObjectModel;

namespace PhoneBook.Views;

public partial class ItemsSetPage : ContentPage
{
    AppDatabase database;
    public ObservableCollection<Item> Items { get; set; } = new();
    public ItemsSetPage(AppDatabase todoItemDatabase)
    {
        InitializeComponent();
        database = todoItemDatabase;
        BindingContext = this;
    }


    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var items = await database.GetItemsAsync();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Items.Clear();
            foreach(var item in items) Items.Add(item);
        });
    }
    async void OnItemAdded(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ItemPage), true, new Dictionary<string, object>
        {
            [nameof(ItemPage.Item)] = new Item()
        });
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if(e.CurrentSelection.FirstOrDefault() is not Item item)
            return;

        await Shell.Current.GoToAsync(nameof(ItemPage), true, new Dictionary<string, object>
        {
            ["Item"] = item
        });
    }
}