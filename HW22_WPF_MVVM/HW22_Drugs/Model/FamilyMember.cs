﻿using System.Collections.ObjectModel;

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


    private ObservableCollection<MedicationCourse> _medicationCourses = new();
    public ObservableCollection<MedicationCourse> MedicationCourses
    {
        get => _medicationCourses;
        private set { _medicationCourses = value; OnPropChanged(nameof(MedicationCourses)); }
    }
}