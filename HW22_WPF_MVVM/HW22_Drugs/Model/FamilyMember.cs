using System.Collections.ObjectModel;

namespace HW22_Drugs.Model;

public class FamilyMember : BaseEntity
{
    private string _name = string.Empty;
    public string Name
    {
        get => _name;
        set { _name = value; OnPropChanged(nameof(Name)); }
    }



    private DateTime _dateOfBirth;
    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set { _dateOfBirth = value; OnPropChanged(nameof(DateOfBirth)); }
    }



    private ObservableCollection<MedicationCourse> _medicationCourses = new();
    public ObservableCollection<MedicationCourse> MedicationCourses
    {
        get => _medicationCourses;
        set { _medicationCourses = value; OnPropChanged(nameof(MedicationCourses)); }
    }



    private ObservableCollection<SheduledMedicationIntake> _scheduledMedicationIntakes = new();
    public ObservableCollection<SheduledMedicationIntake> ScheduledMedicationIntakes
    {
        get => _scheduledMedicationIntakes;
        set { _scheduledMedicationIntakes = value; OnPropChanged(nameof(ScheduledMedicationIntakes)); }
    }
}