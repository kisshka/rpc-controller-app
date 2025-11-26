using Microsoft.EntityFrameworkCore;

namespace RpcControllerClient.Models
{
    public class RpcClientContext : DbContext
    {
        public DbSet<Devices> Devices { get; set; }
        public DbSet<Pupils> Pupils { get; set; }
        public DbSet<Visiting> Visitings { get; set; }

        public RpcClientContext(DbContextOptions<RpcClientContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Visiting>()
                .HasMany(p => p.Pupils)
                .WithMany(v => v.Visitings)
                .UsingEntity<Dictionary<string, object>>(
                    "VisitingPupils",
                    j => j.HasOne<Pupils>().WithMany().HasForeignKey("PupilsId"),
                    j => j.HasOne<Visiting>().WithMany().HasForeignKey("VisitingId"),
                    j =>
                    {
                        j.Property<int>("VisitingPupilsId")
                        .ValueGeneratedOnAdd();
                        j.HasKey("VisitingPupilsId");
                    }
                );
        }




    }
}
