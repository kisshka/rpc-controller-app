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
        }




    }
}
