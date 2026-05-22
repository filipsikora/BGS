using Microsoft.EntityFrameworkCore;
using BGS.Persistence.Entities;

namespace BGS.Persistence.Context
{
    public class BgsDBContext : DbContext
    {
        public DbSet<GameEntity> Games { get; set; }

        public BgsDBContext(DbContextOptions<BgsDBContext> options) : base(options)
        {

        }
    }
}