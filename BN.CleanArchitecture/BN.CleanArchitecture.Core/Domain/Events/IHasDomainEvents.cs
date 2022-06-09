namespace BN.CleanArchitecture.Core.Domain.Events;

public interface IHasDomainEvents
{
    public HashSet<DomainEvent> DomainEvents { get; }
}