using BasicIdentityCustomApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasicIdentityCustomApi.Data
{
    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> option) : base(option)
        {
            
        }

        public DbSet<UserEntity> Users { get; set; }


    }
}
