namespace HW17_WPF_Calc;
public class OpMetainfo
{
    public string Name { get; }

    public int ArgsCount { get; }

    public OpMetainfo(string name, int argsCount)
    {
        Name = name;
        ArgsCount = argsCount;
    }
}