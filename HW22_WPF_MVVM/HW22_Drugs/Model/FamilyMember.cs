namespace HW22_Drugs.Model;

public class FamilyMember : BaseEntity
{
    private string _name = string.Empty;
    public string Name
    {
        get => _name;
        private set { _name = value; OnPropChanged(nameof(Name)); }
    }



    private DateTime _dateOfBirth;
    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        private set { _dateOfBirth = value; OnPropChanged(nameof(DateOfBirth)); }
    }
}