namespace BN.CleanArchitecture.Core.Domain.Cqrs;

public interface IUpdateCommand<TRequest, TResponse> : ICommand<TResponse>
    where TRequest : notnull
    where TResponse : notnull
{
    public TRequest Model { get; init; }
}