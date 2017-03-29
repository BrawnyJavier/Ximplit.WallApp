using System.Data.Entity;
using WallApp.classes;
namespace WallApp.DataModel
{
    public class WallAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }

    }
}
