using HW22_Drugs.Model;
using HW22_Drugs.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HW22_Drugs.Windows;

public partial class MedicationCourseUpsertDialog : Window
{
    public MedicationCourseViewModel EntityViewModel { get; set; }

    public MedicationCourseUpsertDialog(Window ownedWindow, MedicationCourse? medicationCourse, string title)
    {
        InitializeComponent();
        Owner = ownedWindow;
        Title = title;
        DataContext = EntityViewModel = new MedicationCourseViewModel(this, medicationCourse);
    }
}
