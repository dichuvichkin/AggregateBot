using Microsoft.EntityFrameworkCore;

namespace AggregateBot.Models.User
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }

        public UserContext(DbContextOptions options) : base(options)
        {
        }
    }
}