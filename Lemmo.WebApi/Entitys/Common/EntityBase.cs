namespace Lemmo.WebApi.Entitys.Common
{
    public class EntityBase
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}


