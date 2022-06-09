namespace BN.CleanArchitecture.Core.Domain.Entities;

public interface IEntity
{
}

public interface IEntity<TKey> : IEntity
{
    public TKey Id { get; }
}