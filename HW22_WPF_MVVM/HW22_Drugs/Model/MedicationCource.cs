using System.Collections.ObjectModel;

namespace HW22_Drugs.Model;

public class MedicationCourse : BaseEntity
{
    private string _medication = string.Empty;
    public string Medication
    {
        get => _medication;
        set { _medication = value; OnPropChanged(nameof(Medication)); }
    }



    private string _medicationDescription = string.Empty;
    public string MedicationDescription
    {
        get => _medicationDescription;
        set { _medicationDescription = value; OnPropChanged(nameof(MedicationDescription)); }
    }



    private string _medicationGroup = string.Empty;
    public string MedicationGroup
    {
        get => _medicationGroup;
        set { _medicationGroup = value; OnPropChanged(nameof(MedicationGroup)); }
    }

    
    
    private string _intakeInstruction = string.Empty;
    public string IntakeInstruction
    {
        get => _intakeInstruction;
        set { _intakeInstruction = value; OnPropChanged(nameof(IntakeInstruction)); }
    }



    private DateTime _startAt;
    public DateTime StartAt
    {
        get => _startAt;
        set { _startAt = value; OnPropChanged(nameof(StartAt)); }
    }



    private int _totalDozesCount;
    public int TotalDozesCount
    {
        get => _totalDozesCount;
        set { _totalDozesCount = value; OnPropChanged(nameof(TotalDozesCount)); }
    }



    private TimeSpan _timeToNextDose;
    public TimeSpan TimeToNextDose
    {
        get => _timeToNextDose;
        set { _timeToNextDose = value; OnPropChanged(nameof(TimeToNextDose)); }
    }
}