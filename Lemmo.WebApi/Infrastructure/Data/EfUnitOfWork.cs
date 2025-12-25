using Microsoft.EntityFrameworkCore.Storage;

namespace Lemmo.WebApi.Infrastructure.Data
{
    public class EfUnitOfWork(ApplicationDbContext db)
    {
        private IDbContextTransaction? _tx;

        public async Task BeginAsync(CancellationToken ct)
        {
            _tx = await db.Database.BeginTransactionAsync(ct);
        }

        public async Task CommitAsync(CancellationToken ct)
        {
            await db.SaveChangesAsync(ct);
            if (_tx != null)
                await _tx.CommitAsync(ct);
        }

        public async Task RollbackAsync(CancellationToken ct)
        {
            if (_tx != null)
                await _tx.RollbackAsync(ct);
        }
    }
}
