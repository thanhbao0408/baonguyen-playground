using MediatR;

namespace BN.CleanArchitecture.Core.Domain.Events;

public interface IDomainEvent : INotification
{
    DateTime CreatedAt { get; }
    IDictionary<string, object> MetaData { get; }
}