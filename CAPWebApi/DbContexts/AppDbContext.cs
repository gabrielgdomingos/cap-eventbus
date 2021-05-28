using Microsoft.EntityFrameworkCore;

namespace CAPWebApi.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
