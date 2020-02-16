using KnowledgeBase.Core.Entitties;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBase.Core.Data
{
    public class KnowledgeBaseDbContext : DbContext
    {
        public KnowledgeBaseDbContext(DbContextOptions<KnowledgeBaseDbContext> options)
            : base(options)
        {
        }

        public DbSet<Information> Informations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Information>()
                .Property(x => x.CreatedDate)
                .HasDefaultValueSql("getdate()");
        }
    }
}