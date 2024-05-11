namespace HW17_Calculator;

public class CalcAgrInPercents : CalcArg
{
    public CalcAgrInPercents(double value) : base(value) { }

    public override string ToString() => $"({Value} %)";
}
