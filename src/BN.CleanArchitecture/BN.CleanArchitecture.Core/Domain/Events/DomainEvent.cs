namespace BN.CleanArchitecture.Core.Domain.Events;

public abstract class DomainEvent : IDomainEvent
{
    public string? EventType => GetType().FullName;
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public IDictionary<string, object> MetaData { get; } = new Dictionary<string, object>();
}