using CommonLib.Entities;
using CommonLib.Enums;
using Microsoft.EntityFrameworkCore;

namespace CommonLib.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Application> Apps => Set<Application>();
        public DbSet<Department> Departaments => Set<Department>();
        public DbSet<User> Users => Set<User>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureApplications(modelBuilder);
            ConfigureDepartmentTypes(modelBuilder);
            ConfigureUsers(modelBuilder);
        }

        private void ConfigureApplications(ModelBuilder builder)
        {
            var entity = builder.Entity<Application>()
                .ToTable("application");

            entity.Property(p => p.Id).HasColumnName("id");
            entity.Property(p => p.Subject).HasColumnName("subject");
            entity.Property(p => p.Description).HasColumnName("description");
            entity.Property(p => p.Status).HasColumnName("status_id");
            entity.Property(p => p.ApplicationTypeId).HasColumnName("app_type_id");
            entity.Property(p => p.DepartamentId).HasColumnName("department_id");
            entity.Property(p => p.ApplicantId).HasColumnName("applicant_id");
            entity.Property(p => p.ExecutorId).HasColumnName("executor_id");
            entity.Property(p => p.DateCreate).HasColumnName("date_create");
            entity.Property(p => p.DateConfirm).HasColumnName("date_confirm");
            entity.Property(p => p.DateClose).HasColumnName("date_close");
        }
        private void ConfigureDepartmentTypes(ModelBuilder builder)
        {
            var entity = builder.Entity<Department>()
                .ToTable("department_type");

            entity.Property(p => p.Id).HasColumnName("id");
            entity.Property(p => p.Name).HasColumnName("name");
        }
        private void ConfigureUsers(ModelBuilder builder)
        {
            var entity = builder.Entity<User>()
                .ToTable("user");

            entity.Property(p => p.Id).HasColumnName("id");
            entity.Property(p => p.TypeId).HasColumnName("type_id");
            entity.Property(p => p.FirstName).HasColumnName("name");
            entity.Property(p => p.SecondName).HasColumnName("surname");
            entity.Property(p => p.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(p => p.Address).HasColumnName("address");
            entity.Property(p => p.Phone).HasColumnName("phone");
            entity.Property(p => p.Email).HasColumnName("email");
            entity.Property(p => p.RegistrationDate).HasColumnName("registration_date");
            entity.Property(p => p.Login).HasColumnName("login");
            entity.Property(p => p.Password).HasColumnName("password");
            entity.Property(p => p.Balance).HasColumnName("balance");
        }
    }
}
