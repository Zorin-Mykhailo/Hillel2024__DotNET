using HW22_Drugs.Contracts;
using HW22_Drugs.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HW22_Drugs.ViewModel;

public partial class AppViewModel
{

    private IFileService _fileService;

    private IDialogService _dialogService;


    public ObservableCollection<FamilyMember> FamilyMembers { get; set; }

    
    
    private FamilyMember? _selectedFamilyMember;

    public FamilyMember? SelectedFamilyMember
    {
        get => _selectedFamilyMember;
        set { _selectedFamilyMember = value; OnPropertyChanged(nameof(SelectedFamilyMember)); }
    }


    public AppViewModel(IDialogService dialogService, IFileService fileService)
    {
        _dialogService = dialogService;
        _fileService = fileService;

        FamilyMembers =
            [
                new(){ Name = "Пупкін Василь", DateOfBirth = new (1980, 06, 15), UUID = Guid.NewGuid() },
                new(){ Name = "Пупкіна Марія", DateOfBirth = new (1983, 02, 27), UUID = Guid.NewGuid() }
            ];
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
}


public partial class AppViewModel
{
    // Команда збереження даних
    private RelayCommand _fileSaveCommand;
    public RelayCommand FileSaveCommand
    {
        get
        {
            return _fileSaveCommand ??= new RelayCommand(obj =>
            {
                try
                {
                    if(_dialogService.SaveFileDialog() == true)
                    {
                        _fileService.Save(_dialogService.FilePath, FamilyMembers.ToList());
                        _dialogService.ShowMessage("Файл збережено");
                    }
                }
                catch(Exception ex)
                {
                    _dialogService.ShowMessage(ex.Message);
                }
            });
        }
    }

    
    
    // Команда завантаження даних
    private RelayCommand _fileOpenCommand;
    public RelayCommand FileOpenCommand
    {
        get
        {
            return _fileOpenCommand ??= new RelayCommand(obj =>
            {
                try
                {
                    if(_dialogService.OpenFileDialog() == true)
                    {
                        List<FamilyMember> familyMembers = _fileService.Open(_dialogService.FilePath);
                        FamilyMembers.Clear();
                        familyMembers.ForEach(e => FamilyMembers.Add(e));
                        _dialogService.ShowMessage("Файл відкрито");
                    }
                }
                catch(Exception ex)
                {
                    _dialogService.ShowMessage(ex.Message);
                }
            });
        }
    }



    // Команда додавання нового об'єкту
    private RelayCommand _addCommand;
    public RelayCommand AddCommand
    {
        get
        {
            RelayCommand relayCommand = _addCommand ??= new (obj =>
            {
                FamilyMember familyMember = new ();
                FamilyMembers.Insert(0, familyMember);
                SelectedFamilyMember = familyMember;
            });
            return relayCommand;
        }
    }

    
    
    // Команда видалення об'єкту
    private RelayCommand _removeCommand;
    public RelayCommand RemoveCommand
    {
        get
        {
            return _removeCommand ??= new RelayCommand(obj =>
            {
                FamilyMember? familyMember = obj as FamilyMember;
                if (familyMember != null) FamilyMembers.Remove(familyMember);
            },
            (obj) => SelectedFamilyMember != null);
        }
    }
}