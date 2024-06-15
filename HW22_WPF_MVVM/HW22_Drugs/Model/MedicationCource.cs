namespace HW22_Drugs.Model;

public class MedicationCourse : BaseEntity
{
    private FamilyMember _who = default!;
    public FamilyMember Who
    {
        get => _who;
        private set { _who = value; OnPropChanged(nameof(Who)); }
    }



    private string _what = string.Empty;
    public string What
    {
        get => _what;
        private set { _what = value; OnPropChanged(nameof(What)); }
    }



    private string _how = string.Empty;
    public string How
    {
        get => _how;
        private set { _how = value; OnPropChanged(nameof(How)); }
    }



    private DateTime _startAt;
    public DateTime StartAt
    {
        get => _startAt;
        private set { _startAt = value; OnPropChanged(nameof(StartAt)); }
    }



    private int _totalDozesCount;
    public int TotalDozesCount
    {
        get => _totalDozesCount;
        private set { _totalDozesCount = value; OnPropChanged(nameof(TotalDozesCount)); }
    }



    private TimeSpan _timeToNextDose;
    public TimeSpan TimeToNextDose
    {
        get => _timeToNextDose;
        private set { _timeToNextDose = value; OnPropChanged(nameof(TimeToNextDose)); }
    }
}