namespace HW17_WPF_Calc;

public static class CalcStackExt
{
    public static string AsStr(this Stack<CalcItem> calcStack)
    {
        if(calcStack == null) return string.Empty;

        return string.Join(", ", calcStack.Select(x => x.ToString()));
    }
}