using HW22_Drugs.Converters;

namespace HW22_Drugs.Model;

public class SheduledMedicationIntake : BaseEntity
{
    private MedicationCourse _medicationCourse = default!;
    public MedicationCourse MedicationCourse
    {
        get => _medicationCourse;
        set { _medicationCourse = value; OnPropChanged(nameof(MedicationCourse)); }
    }

    private DateTime _planedTime;
    public DateTime PlanedTime
    {
        get => _planedTime;
        set { _planedTime = value; OnPropChanged(nameof(PlanedTime)); }
    }

    
    
    private DateTime? _factTime;
    public DateTime? FactTime
    {
        get => _factTime;
        set { _factTime = value; OnPropChanged(nameof(FactTime)); }
    }



    private EMedicationIntakeResult? _result;
    public EMedicationIntakeResult? Result
    {
        get => _result;
        set { _result = value; OnPropChanged(nameof(Result)); }
    }
}


public enum EMedicationIntakeResult
{
    [FriendlyName("✔ Прийнято")]
    Taked,
    [FriendlyName("❌ ПРОПУЩЕНО")]
    Skiped
}