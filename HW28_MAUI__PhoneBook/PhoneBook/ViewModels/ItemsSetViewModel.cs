using PhoneBook.Data;
using PhoneBook.Models;
using PhoneBook.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PhoneBook.ViewModels;

public partial class ItemsSetViewModel : INotifyPropertyChanged
{
    private AppDatabase _appDb;

    public ObservableCollection<Item> Items { get; set; } = new();

    public ICommand NewItemCommand { get; private set; }

    public ICommand ItemSelectedCommand { get; private set; }
}



public partial class ItemsSetViewModel
{
    private ContentPage _parentPage;

    public ItemsSetViewModel(ContentPage parentPage, AppDatabase appDb)
    {
        _appDb = appDb;
        _parentPage = parentPage;
        NewItemCommand = NewItemCommandInit();
        ItemSelectedCommand = ItemSelectedCommandInit();
    }

    private Command ItemSelectedCommandInit()
    {
        Action<object> execute = async obj =>
        {
            if(obj is not null and Item item)
            {
                ItemViewModel itemViewModel = new(_appDb, item);

                ItemPage page = new ();
                page.ViewModel = itemViewModel;
                page.BindingContext = itemViewModel;

                await _parentPage.Navigation.PushAsync(page);
            }
        };
        Func<object, bool> canExecute = obj => obj is not null and Item;

        return new(execute, canExecute);
    }


    private Command NewItemCommandInit()
    {
        Action execute = async () =>
        {
            ItemViewModel itemViewModel = new(_appDb);

            ItemPage page = new ();
            page.ViewModel = itemViewModel;
            page.BindingContext = itemViewModel;
            
            await _parentPage.Navigation.PushAsync(page);
        };
        Func<bool> canExecute = () => true;

        return new(execute, canExecute);
    }



    public async Task UpdateItemsFromDB()
    {
        if(_appDb == null) return;
        var items = await _appDb.GetItemsAsync();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Items.Clear();
            items.ForEach(e => Items.Add(e));
        });
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    


    bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
    {
        if(Object.Equals(storage, value)) return false;

        storage = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        return true;
    }
}