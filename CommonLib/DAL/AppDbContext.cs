﻿using CommonLib.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommonLib.DAL;

public class AppDbContext : DbContext
{
    public DbSet<Application> Apps => Set<Application>();
    public DbSet<Department> Departaments => Set<Department>();
    public DbSet<User> Users => Set<User>();
    public DbSet<NotificationType> NotificationTypes => Set<NotificationType>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<EmployeeInfo> EmployeeInfos => Set<EmployeeInfo>();
   
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureApplications(modelBuilder);
        ConfigureDepartmentTypes(modelBuilder);
        ConfigureUsers(modelBuilder);
        ConfigureNotificationTypes(modelBuilder);
        ConfigureMessages(modelBuilder);
        ConfigureEmployers(modelBuilder);
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
        entity.Property(p => p.DepartmentId).HasColumnName("department_id");
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
            .ToTable("users");

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
        entity.Property(p => p.MessagingMethodId).HasColumnName("messaging_method_id");
        entity.Property(p => p.MessagingDestination).HasColumnName("messaging_destination");
    }
    private void ConfigureNotificationTypes(ModelBuilder builder)
    {
        var entity = builder.Entity<NotificationType>()
            .ToTable("notification_type");

        entity.Property(p => p.Id).HasColumnName("id");
        entity.Property(p => p.Name).HasColumnName("name");
    }
    private void ConfigureMessages(ModelBuilder builder)
    {
        var entity = builder.Entity<Message>()
            .ToTable("messages");

        entity.Property(p => p.Id).HasColumnName("id");
        entity.Property(p => p.ApplicationId).HasColumnName("application_id");
        entity.Property(p => p.Body).HasColumnName("body");
        entity.Property(p => p.Subject).HasColumnName("subject");
        entity.Property(p => p.MessagingMethodId).HasColumnName("notification_type_id");
        entity.Property(p => p.Destination).HasColumnName("destination");
    }
    private void ConfigureEmployers(ModelBuilder builder)
    {
        var entity = builder.Entity<EmployeeInfo>()
            .ToTable("employers_info");

        entity.Property(p => p.EmployeeUserId).HasColumnName("user_id");
        entity.Property(p => p.DepartmentId).HasColumnName("department_id");
        entity.Property(p => p.Position).HasColumnName("position");
    }
}
