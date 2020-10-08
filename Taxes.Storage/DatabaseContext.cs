using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Taxes.Storage.Entities;

namespace Taxes.Storage
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Municipality>(municipality =>
            {
                municipality.HasKey(m => m.Id);
                municipality.Property(m => m.Name).IsRequired();
            });

            modelBuilder.Entity<Tax>(tax =>
            {
                tax.HasKey(m => m.Id);

                tax.Property(m => m.MunicipalityId).IsRequired();
                tax.HasOne(m => m.Municipality)
                    .WithMany(m => m.Taxes)
                    .HasForeignKey(m => m.MunicipalityId);

                tax.Property(m => m.PeriodStartDate).IsRequired();
                tax.Property(m => m.PeriodEndDate).IsRequired();
                tax.Property(m => m.Value).IsRequired();
                tax.Property(m => m.Value).HasColumnType("decimal(2,2)");
            });
        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
