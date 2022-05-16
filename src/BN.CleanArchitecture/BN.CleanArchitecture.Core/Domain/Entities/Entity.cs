namespace BN.CleanArchitecture.Core.Domain.Entities;

public class Entity : IEntity
{
}

public class Entity<TKey> : Entity, IEntity<TKey>
{
    public virtual TKey Id { get; protected set; }

    protected Entity() { }

    protected Entity(TKey id)
    {
        Id = id;
    }

    public override string ToString()
    {
        return $"[ENTITY: {GetType().Name}] Id = {Id}";
    }
}