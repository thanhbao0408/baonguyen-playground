using MediatR;

namespace BN.CleanArchitecture.Core.Domain.Events;

public class EventWrapper : INotification
{
    public EventWrapper(IDomainEvent @event)
    {
        Event = @event;
    }

    public IDomainEvent Event { get; }
}