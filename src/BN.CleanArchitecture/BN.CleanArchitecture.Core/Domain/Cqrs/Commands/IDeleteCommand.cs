namespace BN.CleanArchitecture.Core.Domain.Cqrs;

public interface IDeleteCommand<TId, TResponse> : ICommand<TResponse>
    where TId : struct
    where TResponse : notnull
{
    public TId Id { get; init; }
}