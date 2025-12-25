using Lemmo.Domain.Common;
using Lemmo.Domain.Users.ValueObjects;

namespace Lemmo.Domain.Users.Specifications
{
    public class UserByTelegramIdSpecification : Specification<User>
    {
        public UserByTelegramIdSpecification(TelegramId telegramId): base(u => u.TelegramId == telegramId)
        {
            IsNotTracking = true;
        }
    }
}
