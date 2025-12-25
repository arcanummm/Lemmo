using Lemmo.WebApi.Entitys.Common;

namespace Lemmo.WebApi.Entitys
{
    public class PreRegistrModel : EntityBase
    {
        public long TelegramId { get; set; }
        public string PhoneNumber { get; set; } 
        public string? PasswordHash { get; set; } 
    }
}
