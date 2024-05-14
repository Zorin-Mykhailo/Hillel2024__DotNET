namespace HW17_WPF_Calc;

public class CalcAgrInPercents : CalcArg
{
    public CalcAgrInPercents(double value) : base(value) { }

    public override string ToString() => $"({Value} %)";
}
