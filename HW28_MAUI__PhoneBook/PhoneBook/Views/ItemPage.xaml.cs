using PhoneBook.Data;
using PhoneBook.Models;

namespace PhoneBook.Views;

public partial class ItemPage : ContentPage
{
    Item item;
    public Item Item
    {
        get => BindingContext as Item;
        set => BindingContext = value;
    }
    AppDatabase database;
    public ItemPage(AppDatabase appDatabase)
    {
        InitializeComponent();
        database = appDatabase;
    }

    async void OnSaveClicked(object sender, EventArgs e)
    {
        if(string.IsNullOrWhiteSpace(Item.Name))
        {
            await DisplayAlert("Name Required", "Please enter a name for the todo item.", "OK");
            return;
        }

        await database.SaveItemAsync(Item);
        await Shell.Current.GoToAsync("..");
    }

    async void OnDeleteClicked(object sender, EventArgs e)
    {
        if(Item.ID == 0)
            return;
        await database.DeleteItemAsync(Item);
        await Shell.Current.GoToAsync("..");
    }

    async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}