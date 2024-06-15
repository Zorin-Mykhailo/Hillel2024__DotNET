using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HW22_Drugs.Model;

public abstract class BaseEntity : INotifyPropertyChanged
{
    private int _id;
    public int Id
    {
        get => _id;
        private set { _id = value; OnPropChanged(nameof(Id)); }
    }



    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropChanged([CallerMemberName] string propName = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
}