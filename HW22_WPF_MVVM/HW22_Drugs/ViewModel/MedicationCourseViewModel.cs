using HW22_Drugs.Contracts;
using HW22_Drugs.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace HW22_Drugs.ViewModel;

public partial class MedicationCourseViewModel
{
    private string? _medication;
    public string? Medication
    {
        get => _medication;
        set { _medication = value; OnPropChanged(nameof(Medication)); }
    }



    private string? _medicationDescription;
    public string? MedicationDescription
    {
        get => _medicationDescription;
        set { _medicationDescription = value; OnPropChanged(nameof(MedicationDescription)); }
    }



    private string? _medicationGroup;
    public string? MedicationGroup
    {
        get => _medicationGroup;
        set { _medicationGroup = value; OnPropChanged(nameof(MedicationGroup)); }
    }



    private string? _intakeInstruction;
    public string? IntakeInstruction
    {
        get => _intakeInstruction;
        set { _intakeInstruction = value; OnPropChanged(nameof(IntakeInstruction)); }
    }



    private DateTime? _startAt;
    public DateTime? StartAt
    {
        get => _startAt;
        set { _startAt = value; OnPropChanged(nameof(StartAt)); }
    }



    private int? _totalDozesCount;
    public int? TotalDozesCount
    {
        get => _totalDozesCount;
        set { _totalDozesCount = value; OnPropChanged(nameof(TotalDozesCount)); }
    }



    private TimeSpan? _timeToNextDose;
    public TimeSpan? TimeToNextDose
    {
        get => _timeToNextDose;
        set { _timeToNextDose = value; OnPropChanged(nameof(TimeToNextDose)); }
    }
}



public partial class MedicationCourseViewModel
{
    private Window _ownedWindow;

    public MedicationCourseViewModel(Window ownedWindow, MedicationCourse? entity = null)
    {
        _ownedWindow = ownedWindow;
        if (entity != null) UpdateFromEntity(entity);
    }


    public void UpdateFromEntity(MedicationCourse entity)
    {
        Medication = entity.Medication;
        MedicationDescription = entity.MedicationDescription;
        MedicationGroup = entity.MedicationGroup;
        IntakeInstruction = entity.IntakeInstruction;
        StartAt = entity.StartAt;
        TotalDozesCount = entity.TotalDozesCount;
        TimeToNextDose = entity.TimeToNextDose;
    }

    public void UpdateEntity(MedicationCourse entity)
    {
        entity.Medication = Medication!;
        entity.MedicationDescription = MedicationDescription!;
        entity.MedicationGroup = MedicationGroup!;
        entity.IntakeInstruction = IntakeInstruction!;
        entity.StartAt = (DateTime)StartAt!;
        entity.TotalDozesCount = (int)TotalDozesCount!;
        entity.TimeToNextDose = (TimeSpan)TimeToNextDose!;
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
                if(obj == this)
                    return !string.IsNullOrWhiteSpace(Medication)
                    && !string.IsNullOrWhiteSpace(MedicationDescription)
                    && !string.IsNullOrWhiteSpace(MedicationGroup)
                    && !string.IsNullOrWhiteSpace(IntakeInstruction)
                    && StartAt != null
                    && TotalDozesCount != null
                    && TimeToNextDose != null
                    ;
                return false;
            });
            return relayCommand;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropChanged([CallerMemberName] string prop = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
}