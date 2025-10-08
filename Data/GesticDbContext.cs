using GesticApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GesticApi.Data
{
    /// <summary>
    ///     Contexto de base de datos para el catálogo de servicios TIC. Este
    ///     contexto define las tablas, claves y relaciones de acuerdo al
    ///     Modelo Entidad–Relación actualizado. Se configuran los tipos
    ///     enumerados de PostgreSQL para almacenar los estados como texto.
    /// </summary>
    public class GesticDbContext : DbContext
    {
        public GesticDbContext(DbContextOptions<GesticDbContext> options)
            : base(options)
        {
        }

        // Conjuntos de entidades (tablas)
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Service> Services => Set<Service>();
        public DbSet<ServiceHistory> ServiceHistories => Set<ServiceHistory>();
        public DbSet<Request> Requests => Set<Request>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Roles
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");
                entity.Property(e => e.Id).HasColumnName("role_id");
                entity.Property(e => e.Name).HasColumnName("name").IsRequired();
                entity.Property(e => e.Description).HasColumnName("description");
            });

            // Users
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.Property(e => e.Id).HasColumnName("user_id");
                entity.Property(e => e.Username).HasColumnName("username").IsRequired();
                entity.Property(e => e.PasswordHash).HasColumnName("password_hash").IsRequired();
                entity.Property(e => e.Email).HasColumnName("email").IsRequired();
                entity.Property(e => e.RoleId).HasColumnName("role_id");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");

                entity.HasOne(d => d.Role)
                      .WithMany(p => p.Users)
                      .HasForeignKey(d => d.RoleId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("fk_users_role");
            });

            // Categories
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");
                entity.Property(e => e.Id).HasColumnName("category_id");
                entity.Property(e => e.Name).HasColumnName("name").IsRequired();
                entity.Property(e => e.Description).HasColumnName("description");
            });

            // Services
            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("services");
                entity.Property(e => e.Id).HasColumnName("service_id");
                entity.Property(e => e.Name).HasColumnName("name").IsRequired();
                entity.Property(e => e.Description).HasColumnName("description").IsRequired();
                entity.Property(e => e.CategoryId).HasColumnName("category_id");
                entity.Property(e => e.Sla).HasColumnName("sla");
                entity.Property(e => e.Status)
                      .HasColumnName("status")
                      .HasColumnType("service_status")
                      .HasConversion<string>();
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");

                entity.HasOne(d => d.Category)
                      .WithMany(p => p.Services)
                      .HasForeignKey(d => d.CategoryId)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("fk_services_category");

                entity.HasOne(d => d.CreatedByUser)
                      .WithMany(p => p.ServicesCreated)
                      .HasForeignKey(d => d.CreatedBy)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("fk_services_user");
            });

            // Service histories
            modelBuilder.Entity<ServiceHistory>(entity =>
            {
                entity.ToTable("service_history");
                entity.Property(e => e.Id).HasColumnName("history_id");
                entity.Property(e => e.ServiceId).HasColumnName("service_id");
                entity.Property(e => e.ChangeDate).HasColumnName("change_date");
                entity.Property(e => e.ChangedBy).HasColumnName("changed_by");
                entity.Property(e => e.OldValue).HasColumnName("old_value");
                entity.Property(e => e.NewValue).HasColumnName("new_value");

                entity.HasOne(d => d.Service)
                      .WithMany(p => p.Histories)
                      .HasForeignKey(d => d.ServiceId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_history_service");

                entity.HasOne(d => d.ChangedByUser)
                      .WithMany()
                      .HasForeignKey(d => d.ChangedBy)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("fk_history_user");
            });

            // Requests
            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("requests");
                entity.Property(e => e.Id).HasColumnName("request_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.ServiceId).HasColumnName("service_id");
                entity.Property(e => e.RequestDate).HasColumnName("request_date");
                entity.Property(e => e.Status)
                      .HasColumnName("status")
                      .HasColumnType("request_status")
                      .HasConversion<string>();
                entity.Property(e => e.Details).HasColumnName("details");

                entity.HasOne(d => d.User)
                      .WithMany(p => p.Requests)
                      .HasForeignKey(d => d.UserId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("fk_requests_user");

                entity.HasOne(d => d.Service)
                      .WithMany(p => p.Requests)
                      .HasForeignKey(d => d.ServiceId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("fk_requests_service");
            });
        }
    }
}