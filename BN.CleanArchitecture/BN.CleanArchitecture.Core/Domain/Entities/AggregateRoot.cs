using BN.CleanArchitecture.Core.Domain.Events;

namespace BN.CleanArchitecture.Core.Domain.Entities;

public class AggregateRoot : Entity, IAggregateRoot, IHasDomainEvents
{
    public HashSet<DomainEvent> DomainEvents { get; private set; }

    protected AggregateRoot()
    {
        DomainEvents ??= new HashSet<DomainEvent>();
    }

    public void AddDomainEvent(DomainEvent eventItem)
    {
        DomainEvents ??= new HashSet<DomainEvent>();
        DomainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(DomainEvent eventItem)
    {
        DomainEvents?.Remove(eventItem);
    }
}

public class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot<TKey>, IHasDomainEvents
{
    public HashSet<DomainEvent> DomainEvents { get; private set; }

    protected AggregateRoot()
    {
        DomainEvents ??= new HashSet<DomainEvent>();
    }

    protected AggregateRoot(TKey id) : base(id)
    {
        DomainEvents ??= new HashSet<DomainEvent>();
    }

    public void AddDomainEvent(DomainEvent eventItem)
    {
        DomainEvents ??= new HashSet<DomainEvent>();
        DomainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(DomainEvent eventItem)
    {
        DomainEvents?.Remove(eventItem);
    }
}