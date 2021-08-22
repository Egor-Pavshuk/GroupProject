using GroupProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupProject
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Elective> Electives { get; set; }
        public ApplicationContext()
        {
            //Database.EnsureCreated();
        }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Test;Username=postgres;Password=Egor1324");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                var students = modelBuilder.Entity<Student>();
                var groups = modelBuilder.Entity<Group>();
                var electives = modelBuilder.Entity<Elective>();

                students.HasOne(s => s.Group)
                    .WithMany(g => g.Students)
                    .HasForeignKey(s => s.GroupId);
                students.HasMany(s => s.Electives)
                    .WithMany(e => e.Students)
                    .UsingEntity(j => j.ToTable("Blank"));
                students.Property("Name").IsRequired();
                students.Property("Surname").IsRequired();
                groups.Property("Name").IsRequired();
                electives.Property("Name").IsRequired();

               students.ToTable("Students");
               groups.ToTable("Groups");
               electives.ToTable("Electives");
            }
    }
}