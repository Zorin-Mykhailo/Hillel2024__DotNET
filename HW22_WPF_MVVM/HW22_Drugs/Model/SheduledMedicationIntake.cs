﻿namespace HW22_Drugs.Model;

public class SheduledMedicationIntake : BaseEntity
{
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
}