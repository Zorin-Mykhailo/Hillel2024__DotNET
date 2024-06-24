using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HW22_Drugs.Model;

public abstract class BaseEntity : INotifyPropertyChanged
{
    private Guid _uuid;
    public Guid UUID
    {
        get => _uuid;
        set { _uuid = value; OnPropChanged(nameof(UUID)); }
    }



    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropChanged([CallerMemberName] string propName = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
}