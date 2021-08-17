using Microsoft.EntityFrameworkCore;

namespace GroupProject
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
            {
                Database.EnsureCreated();
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Test;Username=postgres;Password=Egor1324");
            }
    }
}