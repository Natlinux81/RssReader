using Domain.Interface;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class UnitOfWork(RssReaderDbContext context) : IUnitOfWork
    {
        public void Commit()
        {
            context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}