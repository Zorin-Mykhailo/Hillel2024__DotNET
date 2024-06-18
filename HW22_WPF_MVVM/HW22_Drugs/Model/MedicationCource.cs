using System.Collections.ObjectModel;

namespace HW22_Drugs.Model;

public class MedicationCourse : BaseEntity
{
    private FamilyMember _familyMember = default!;
    public FamilyMember FamilyMember
    {
        get => _familyMember;
        set { _familyMember = value; OnPropChanged(nameof(FamilyMember)); }
    }



    private string _medication = string.Empty;
    public string Medication
    {
        get => _medication;
        set { _medication = value; OnPropChanged(nameof(Medication)); }
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



    private ObservableCollection<SheduledMedicationIntake> _medicationCourses = new();
    public ObservableCollection<SheduledMedicationIntake> MedicationCourses
    {
        get => _medicationCourses;
        set { _medicationCourses = value; OnPropChanged(nameof(MedicationCourses)); }
    }
}