using CommonLib.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CommonLib.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Application> Apps => Set<Application>();
        public DbSet<ApplicationStatus> AppStatus => Set<ApplicationStatus>();
        public DbSet<ApplicationType> AppTypes => Set<ApplicationType>();
        public DbSet<Department> Departaments => Set<Department>();
        public DbSet<User> Users => Set<User>();
        public DbSet<UserType> UserTypes => Set<UserType>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
