namespace Lemmo.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }

        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; private set; } = DateTimeOffset.UtcNow;

        public bool IsDeleted { get; private set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        protected void UpdateTimestamp()
        {
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void MarkDeleted()
        {
            if (IsDeleted) return; 

            IsDeleted = true;
            UpdateTimestamp();
        }

        public void Restore()
        {
            if (!IsDeleted) return;

            IsDeleted = false;
            UpdateTimestamp();
        }
    }
}
