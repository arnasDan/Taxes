using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Taxes.Storage.Entities;

namespace Taxes.Storage
{
    public interface IDatabaseContext
    {
        DbSet<Tax> Taxes { get; set; }
        DbSet<Municipality> Municipalities { get; set; }
        DbSet<T> Set<T>() where T : class;
        Task SaveChangesAsync(CancellationToken cancellation = default);
    }
}