namespace HW17_Calculator;
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