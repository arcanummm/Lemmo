using Lemmo.WebApi.Entitys.Common;

namespace Lemmo.WebApi.Entitys
{
    public class RefreshToken : EntityBase
    {
        public string TokenHash { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }

        public bool IsRevoked { get; set; } = false;
        public DateTime? RevokedAt { get; set; }

        public Guid UserId { get; set; } 
        public User User { get; set; } 

        public string Device { get; set; }
    }
}
