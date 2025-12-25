namespace Lemmo.Domain.Common
{
    public interface IDomainEvent
    {
        DateTimeOffset OccurredOn { get; }
        Guid AggregateId { get; }
    }
}
