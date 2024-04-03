namespace Store.Service;

public interface IRequestHandler<TParam, in TRequest, TResponse>
{
    Task<TResponse> Handle(TParam param, TRequest request, CancellationToken cancellationToken = default);
}

public interface IRequestHandler<in TRequest, TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken = default);
}

public interface IRequestHandler<TResponse>
{
    Task<TResponse> Handle(CancellationToken cancellationToken = default);
}