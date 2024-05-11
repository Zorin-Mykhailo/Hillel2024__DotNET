namespace HW17_Calculator;
public class CalcArg : CalcItem
{
    public double Value { get; set; }

    public CalcArg(double value) => Value = value;

    public override string ToString() => $"({Value})";
}