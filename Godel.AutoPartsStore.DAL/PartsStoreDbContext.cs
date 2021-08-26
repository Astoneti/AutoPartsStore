using Godel.AutoPartsStore.Common.Models;
using Godel.AutoPartsStore.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Godel.AutoPartsStore.DAL
{
    public class PartsStoreDbContext : DbContext
    {
        public DbSet<Part> Parts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public PartsStoreDbContext(DbContextOptions<PartsStoreDbContext> options)
           : base(options)
        {
        }
    }
 }
