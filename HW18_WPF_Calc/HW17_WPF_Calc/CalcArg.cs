namespace HW17_WPF_Calc;
public class CalcArg : CalcItem
{
    public double Value { get; set; }

    public CalcArg(double value) => Value = value;

    public override string ToString() => $"({Value})";
}