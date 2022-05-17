namespace BN.CleanArchitecture.Core.Domain.Cqrs;

public interface IItemQuery<TKey, TResponse> : IQuery<TResponse>
    //where TKey : struct
    where TResponse : notnull
{
    public List<string> Includes { get; init; }
    public TKey Key { get; init; }
}