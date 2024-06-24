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
using HW22_Drugs.Windows;
using System.Windows;

namespace HW22_Drugs.ViewModel;

public partial class AppViewModel
{
    private Window _ownedWindow;

    private IFileService _fileService;

    private IDialogService _dialogService;


    public ObservableCollection<FamilyMember> FamilyMembers { get; set; } = new();

    
    
    private FamilyMember? _selectedFamilyMember;

    public FamilyMember? SelectedFamilyMember
    {
        get => _selectedFamilyMember;
        set { _selectedFamilyMember = value; OnPropertyChanged(nameof(SelectedFamilyMember)); }
    }


    private MedicationCourse? _selectedMedicationCourse;

    public MedicationCourse? SelectedMedicationCourse
    {
        get => _selectedMedicationCourse;
        set { _selectedMedicationCourse = value; OnPropertyChanged(nameof(SelectedMedicationCourse)); }
    }

    private SheduledMedicationIntake? _selectedScheduledMedicationIntake;

    public SheduledMedicationIntake? SelectedScheduledMedicationIntake
    {
        get => _selectedScheduledMedicationIntake;
        set { _selectedScheduledMedicationIntake = value; OnPropertyChanged(nameof(SelectedScheduledMedicationIntake)); }
    }


    public AppViewModel(IDialogService dialogService, IFileService fileService, Window ownedWindow)
    {
        _ownedWindow = ownedWindow;
        _dialogService = dialogService;
        _fileService = fileService;
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
}


public partial class AppViewModel
{
    // Data • Save command
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

    
    
    // Data • Load command
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



    // Family member • Add new command
    private RelayCommand _familyMemberAddCommand;
    public RelayCommand FamilyMemberAddCommand
    {
        get
        {
            RelayCommand relayCommand = _familyMemberAddCommand ??= new (obj =>
            {
                FamilyMemberUpsertDialog familyMemberDialog = new (_ownedWindow, null, "Новий член сім'ї");                
                if(familyMemberDialog.ShowDialog() == true)
                {
                    FamilyMember familyMember = new ();
                    familyMember.UUID = Guid.NewGuid();
                    familyMemberDialog.EntityViewModel.UpdateEntity(familyMember);
                    FamilyMembers.Add(familyMember);
                    SelectedFamilyMember = familyMember;
                }
            });
            return relayCommand;
        }
    }


    // Family member • Edit command
    private RelayCommand _familyMemberEditCommand;
    public RelayCommand FamilyMemberEditCommand
    {
        get
        {
            RelayCommand relayCommand = _familyMemberEditCommand ??= new (obj =>
            {
                FamilyMemberUpsertDialog familyMemberDialog = new (_ownedWindow, SelectedFamilyMember, "Редагування члена сім'ї");
                if(familyMemberDialog.ShowDialog() == true)
                    familyMemberDialog.EntityViewModel.UpdateEntity(SelectedFamilyMember!);
            },
            (obj) => SelectedFamilyMember != null);
            return relayCommand;
        }
    }



    // Family member • Remove command
    private RelayCommand _familyMemberRemoveCommand;
    public RelayCommand FamilyMemberRemoveCommand
    {
        get
        {
            return _familyMemberRemoveCommand ??= new RelayCommand(obj =>
            {
                FamilyMember? familyMember = obj as FamilyMember;
                if (familyMember != null) 
                    FamilyMembers.Remove(familyMember);
            },
            (obj) => SelectedFamilyMember != null);
        }
    }


    // MedicationCourse course • Add new command
    private RelayCommand _medicationCourseAddCommand;
    public RelayCommand MedicationCourseAddCommand
    {
        get
        {
            RelayCommand relayCommand = _medicationCourseAddCommand ??= new (obj =>
            {

                MedicationCourseUpsertDialog medicationCourseUpsertDialog = new(_ownedWindow, null, "Новий курс лікарського засобу");

                if(medicationCourseUpsertDialog.ShowDialog() == true)
                {
                    MedicationCourse medicationCourse = new();
                    medicationCourse.UUID = Guid.NewGuid();

                    medicationCourseUpsertDialog.EntityViewModel.UpdateEntity(medicationCourse);
                    SelectedFamilyMember.MedicationCourses.Add(medicationCourse);
                    SelectedMedicationCourse = medicationCourse;
                    //
                    UpdateSheduledMedicationIntakes(SelectedFamilyMember, SelectedMedicationCourse);
                }
            },
            (obj) => SelectedFamilyMember != null);
            return relayCommand;
        }
    }


    private void UpdateSheduledMedicationIntakes(FamilyMember familyMember, MedicationCourse medicationCourse)
    {
        if(familyMember == null || medicationCourse == null) return;

        Guid medicationCourseId = medicationCourse.UUID;



        foreach(var e in familyMember.ScheduledMedicationIntakes.ToList())
        {
            if(e.MedicationCourse.UUID != medicationCourseId || e.Result != null)
                continue;
            familyMember.ScheduledMedicationIntakes.Remove(e);
        }

        DateTime timeStamp = medicationCourse.StartAt;
        int dozeNumber = 0;

        while(dozeNumber++ < medicationCourse.TotalDozesCount)
        {
            SheduledMedicationIntake sheduledMedicationIntake = new ();
            SelectedFamilyMember.ScheduledMedicationIntakes.Add(sheduledMedicationIntake);
            //
            sheduledMedicationIntake.FactTime = null;
            sheduledMedicationIntake.MedicationCourse = medicationCourse;
            sheduledMedicationIntake.PlanedTime = timeStamp;
            timeStamp += medicationCourse.TimeToNextDose;
            sheduledMedicationIntake.Result = null;
            sheduledMedicationIntake.UUID = Guid.NewGuid();
        }
        var orderedMedicalIntakes = SelectedFamilyMember.ScheduledMedicationIntakes.OrderBy(e => e.PlanedTime).ToList();
        SelectedFamilyMember.ScheduledMedicationIntakes.Clear();
        orderedMedicalIntakes.ForEach(e => SelectedFamilyMember.ScheduledMedicationIntakes.Add(e));
    }


    // Medication course • Edit command
    private RelayCommand _medicationCourseEditCommand;
    public RelayCommand MedicationCourseEditCommand
    {
        get
        {
            RelayCommand relayCommand = _medicationCourseEditCommand ??= new (obj =>
            {
                MedicationCourseUpsertDialog medicationCourseUpsertDialog = new(_ownedWindow, SelectedMedicationCourse, "Редагування курсу лікарського засобу");
                if(medicationCourseUpsertDialog.ShowDialog() == true)
                {
                    medicationCourseUpsertDialog.EntityViewModel.UpdateEntity(SelectedMedicationCourse!);
                    UpdateSheduledMedicationIntakes(SelectedFamilyMember!, SelectedMedicationCourse!);
                }
            },
            (obj) => SelectedFamilyMember != null && SelectedMedicationCourse != null);
            return relayCommand;
        }
    }



    // MedicationCourse course • Remove command
    private RelayCommand _medicationCourseRemoveCommand;
    public RelayCommand MedicationCourseRemoveCommand
    {
        get
        {
            return _medicationCourseRemoveCommand ??= new RelayCommand(obj =>
            {
                MedicationCourse? medicationCourse = obj as MedicationCourse;
                if(medicationCourse != null)
                {
                    SelectedFamilyMember!.MedicationCourses.Remove(medicationCourse);
                    UpdateSheduledMedicationIntakes(SelectedFamilyMember!, SelectedMedicationCourse!);
                }
            },
            (obj) => SelectedFamilyMember != null && SelectedMedicationCourse != null);
        }
    }




    // Medication intaked command
    private RelayCommand _medicationIntakedCommand;
    public RelayCommand MedicationIntakedCommand
    {
        get
        {
            return _medicationIntakedCommand ??= new RelayCommand(obj =>
            {
                SelectedScheduledMedicationIntake.FactTime = DateTime.Now;
                SelectedScheduledMedicationIntake.Result = EMedicationIntakeResult.Taked;
            },
            (obj) => SelectedScheduledMedicationIntake != null && SelectedScheduledMedicationIntake.Result == null);
        }
    }




    // Medication skiped command
    private RelayCommand _medicationSkipedCommand;
    public RelayCommand MedicationSkipedCommand
    {
        get
        {
            return _medicationSkipedCommand ??= new RelayCommand(obj =>
            {
                SelectedScheduledMedicationIntake.FactTime = null;
                SelectedScheduledMedicationIntake.Result = EMedicationIntakeResult.Skiped;
            },
            (obj) => SelectedScheduledMedicationIntake != null && SelectedScheduledMedicationIntake.Result == null);
        }
    }
}