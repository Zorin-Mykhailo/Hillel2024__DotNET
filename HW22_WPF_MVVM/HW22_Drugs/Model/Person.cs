using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HW22_Drugs.Model;
public class Person : INotifyPropertyChanged
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


    private double _weight;
    public double Weight
    {
        get => _weight;
        private set { _weight = value; OnPropChanged(nameof(Weight)); }
    }

    
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropChanged([CallerMemberName]string propName = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
}