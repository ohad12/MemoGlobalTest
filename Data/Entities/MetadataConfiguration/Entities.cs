using Microsoft.EntityFrameworkCore;

namespace MemoGlobalTest.Data.Entities.MetadataConfiguration
{
    public class Entities : DbContext
    {
        public Entities()
        {
        }

        public Entities(DbContextOptions<Entities> options) : base(options) { }
        public virtual DbSet<ReqresUser> ReqresUser { get; set; }
      
        public static string GetConnectionString()
        {
            var configuration = Startup.Configuration;
            var conn = configuration.GetConnectionString("DbConnectionString");
            return conn;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connectionString = GetConnectionString();
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ReqresUser>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();
            });
        }
    }
}

