namespace BN.CleanArchitecture.Core.Domain.Events;

public interface IDomainEventContext
{
    IEnumerable<DomainEvent> GetDomainEvents();
}