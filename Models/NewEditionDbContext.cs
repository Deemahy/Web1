using Microsoft.EntityFrameworkCore;

namespace Web1.Models
{
    public class NewEditionDbContext : DbContext
    {
        public NewEditionDbContext(DbContextOptions<NewEditionDbContext> options)
            : base(options)
        {
        }

    }
}
