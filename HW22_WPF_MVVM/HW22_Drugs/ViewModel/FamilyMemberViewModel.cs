using HW22_Drugs.Contracts;
using HW22_Drugs.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace HW22_Drugs.ViewModel;

public partial class FamilyMemberViewModel
{
    private Guid _uuid;
    public Guid UUID
    {
        get => _uuid;
        set { _uuid = value; OnPropChanged(nameof(UUID)); }
    }

    
    
    private string _name = string.Empty;
    public string Name
    {
        get => _name;
        set { _name = value; OnPropChanged(nameof(Name)); }
    }



    private DateTime? _dateOfBirth;
    public DateTime? DateOfBirth
    {
        get => _dateOfBirth;
        set { _dateOfBirth = value; OnPropChanged(nameof(DateOfBirth)); }
    }
}



public partial class FamilyMemberViewModel
{
    private Window _ownedWindow;

    public FamilyMemberViewModel(Window ownedWindow, FamilyMember? familyMember)
    {
        _ownedWindow = ownedWindow; 
        if(familyMember != null) UpdateFromEntity(familyMember);
    }

    public void UpdateFromEntity(FamilyMember entity)
    {
        UUID = entity.UUID;
        Name = entity.Name;
        DateOfBirth = entity.DateOfBirth;
    }

    public void UpdateEntity(FamilyMember entity)
    {
        entity.Name = Name;
        entity.DateOfBirth = (DateTime)DateOfBirth;
    }

    
    private RelayCommand _dialogOkCommand;
    public RelayCommand DialogOkCommand
    {
        get
        {
            RelayCommand relayCommand = _dialogOkCommand ??= new (obj =>
            {
                _ownedWindow.DialogResult = true;
            },
            (obj) =>
            {
                if(obj != this)
                    return false;
                return !string.IsNullOrWhiteSpace(Name);
            });
            return relayCommand;
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropChanged([CallerMemberName] string prop = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
}