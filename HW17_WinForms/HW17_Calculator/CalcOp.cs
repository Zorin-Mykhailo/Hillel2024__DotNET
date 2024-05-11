namespace HW17_Calculator;

public class CalcOp : CalcItem
{
    public ECalcOperation Kind { get; set; }

    public int ArgsCount { get => Metaifo[Kind].ArgsCount; }

    public CalcOp(ECalcOperation operation) => Kind = operation;

    private readonly static Dictionary<ECalcOperation, OpMetainfo> Metaifo;

    static CalcOp()
    {
        Metaifo = new Dictionary<ECalcOperation, OpMetainfo>
            {
                { ECalcOperation.Plus, new OpMetainfo("+", 2) },
                { ECalcOperation.Minus, new OpMetainfo("-", 2) },
                { ECalcOperation.Multiply, new OpMetainfo("*", 2) },
                { ECalcOperation.Divide, new OpMetainfo("/", 2) },
                { ECalcOperation.Percent, new OpMetainfo("%", 1) },
                { ECalcOperation.Fraction, new OpMetainfo("1/x", 1) },
                { ECalcOperation.Sqrt, new OpMetainfo("Sqrt", 1) },
                { ECalcOperation.Inverse, new OpMetainfo("Inv", 1) },
                { ECalcOperation.Equals, new OpMetainfo("=", 1) },
            };
    }

    public override string ToString() => $"[{Metaifo[Kind].Name}]";
}