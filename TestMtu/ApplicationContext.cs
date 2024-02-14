using Microsoft.EntityFrameworkCore;
using static starteducation.Program;

namespace starteducation
{
    internal class ApplicationContext : DbContext   
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                Program.connectionString,
                ServerVersion.AutoDetect(Program.connectionString)
            );
        }

        public DbSet<User> Users { get; set; }ы
    }
}