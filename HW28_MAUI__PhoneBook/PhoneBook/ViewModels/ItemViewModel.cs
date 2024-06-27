using AppExt;
using PhoneBook.Data;
using PhoneBook.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PhoneBook.ViewModels;

public partial class ItemViewModel : INotifyPropertyChanged
{
    public int ID { get; set; }

    
    
    private string _firstName;
    public string FirstName
    {
        get => _firstName;
        set => SetProperty(ref _firstName, value);
    }


    
    private string _lastName;
    public string LastName
    {
        get => _lastName;
        set => SetProperty(ref _lastName, value);
    }



    private string _phone;
    public string Phone
    {
        get => _phone;
        set => SetProperty(ref _phone, value);
    }


    private string _description;
    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }


    
    public Command SaveCommand { get; private set; }

    public Command DeleteCommand { private set; get; }

    public Command CancelCommand { private set; get; }

    public Item Model { get; set; }

    private AppDatabase _appDb;
}


public partial class ItemViewModel 
{
    public ItemViewModel(AppDatabase appDb)
    {
        _appDb = appDb;
        SaveCommand = SaveCommandInit();
        DeleteCommand = DeleteCommandInit();
        CancelCommand = CancelCommandInit();
    }

    public ItemViewModel(AppDatabase appDb, Item item) : this(appDb)
    {
        Model = item;
        ID = item.ID;
        FirstName = item.FirstName;
        LastName = item.LastName;
        Description = item.Description;
        Phone = item.Phone;
    }

    private Item UpdateModel()
    {
        Model ??= new();

        Model.FirstName = FirstName;
        Model.LastName = LastName;
        Model.Description = Description;
        Model.Phone = Phone;

        return Model;
    }

    


    private Command SaveCommandInit()
    {
        Action execute = async () => 
        {
            await _appDb.SaveItemAsync(UpdateModel());
            await Shell.Current.GoToAsync("..");
        };
        Func<bool> canExecute = () =>
        {
            return !string.IsNullOrWhiteSpace(LastName)
                && !string.IsNullOrWhiteSpace(FirstName)
                && !string.IsNullOrWhiteSpace(Phone); 
        };        

        return new (execute, canExecute);
    }

    private Command DeleteCommandInit()
    {
        Action execute = async () => 
        {
            await _appDb.DeleteItemAsync(Model);
            await Shell.Current.GoToAsync("..");
        };
        Func<bool> canExecute = () => 
        {
            return ID > 0;
        };

        return new (execute, canExecute);
    }

    private Command CancelCommandInit()
    {
        Action execute = async () => { await Shell.Current.GoToAsync(".."); };
        Func<bool> canExecute = () =>  { return true; };

        return new (execute, canExecute);
    }


    public event PropertyChangedEventHandler PropertyChanged;


    protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
    {
        if(Object.Equals(storage, value)) return false;

        storage = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        RefreshCanExecutes();
        return true;
    }

    void RefreshCanExecutes()
    {
        SaveCommand.ChangeCanExecute();
        DeleteCommand.ChangeCanExecute();
        CancelCommand.ChangeCanExecute();
    }
}