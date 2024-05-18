using System.Diagnostics;

namespace HW14.MovieManager.Service.Common;

public class RequestResult<T>
{
    public T? Value { get; set; }

    public bool HasError { get => Exception != null; }

    public Exception? Exception { get; set; }

    private Stopwatch TimeCounter { get; set; } = new();

    public TimeSpan TimeElapsed => TimeCounter.Elapsed;

    public RequestResult() => TimeCounter.Start();

    public void Stop() => TimeCounter.Stop();
}