using Microsoft.EntityFrameworkCore;
using NSE.Catalogo.API.Model;
using NSE.Core.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Catalogo.API.Data
{
    public class CatalagoContext : DbContext, IUnitOfWork
    {
        public CatalagoContext(DbContextOptions<CatalagoContext> options)
            : base(options){ }

        public DbSet<Produto> Produtos { get; set; }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalagoContext).Assembly);
        }

        public async Task<bool> CommitAsync()
        {
           return await base.SaveChangesAsync() > 0;
        }
    }
}
