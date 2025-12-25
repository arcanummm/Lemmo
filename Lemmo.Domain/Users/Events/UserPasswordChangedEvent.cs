using Lemmo.Domain.Common;

namespace Lemmo.Domain.Users.Events
{
    public class UserPasswordChangedEvent(Guid aggregateId) : IDomainEvent
    {
        public Guid AggregateId { get; } = aggregateId;
        public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
    }
}
