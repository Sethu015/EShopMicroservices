using Ordering.Domain.Abstractions;

namespace Ordering.Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdaateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdaateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdaateEntities(DbContext? context)
        {
            if(context is null) return;
            foreach (var item in context.ChangeTracker.Entries<IEntity>())
            {
                if (item.State == EntityState.Added)
                {
                    item.Entity.CreatedBy = "System"; // Replace with actual user context if available
                    item.Entity.CreatedAt = DateTime.UtcNow;
                }

                if (item.State == EntityState.Modified || item.HasChangedOwnedEntities())
                {
                    item.Entity.ModifiedBy = "System"; // Replace with actual user context if available
                    item.Entity.ModifiedAt = DateTime.UtcNow;
                }
            }
        }
    }
}
