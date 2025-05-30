using AgricultureSmart.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.DbAgriContext
{
    public class AgricultureSmartDbContext : DbContext
    {
        public AgricultureSmartDbContext(DbContextOptions<AgricultureSmartDbContext> options)
            : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Engineer> Engineers { get; set; }
        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<EngineerFarmerAssignment> EngineerFarmerAssignments { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketComment> TicketComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure table names
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<UserRole>().ToTable("UserRoles");
            modelBuilder.Entity<Engineer>().ToTable("Engineers");
            modelBuilder.Entity<Farmer>().ToTable("Farmers");
            modelBuilder.Entity<EngineerFarmerAssignment>().ToTable("EngineerFarmerAssignments");
            modelBuilder.Entity<BlogCategory>().ToTable("BlogCategories");
            modelBuilder.Entity<Blog>().ToTable("Blogs");
            modelBuilder.Entity<Ticket>().ToTable("Tickets");
            modelBuilder.Entity<TicketComment>().ToTable("TicketComments");

            // Configure unique constraints
            modelBuilder.Entity<Users>()
                .HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("IX_Users_Email");

            modelBuilder.Entity<Users>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique()
                .HasDatabaseName("IX_Users_PhoneNumber");

            modelBuilder.Entity<Users>()
                .HasIndex(u => u.UserName)
                .IsUnique()
                .HasDatabaseName("IX_Users_UserName");

            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique()
                .HasDatabaseName("IX_Roles_Name");

            modelBuilder.Entity<BlogCategory>()
                .HasIndex(bc => bc.Name)
                .IsUnique()
                .HasDatabaseName("IX_BlogCategories_Name");

            modelBuilder.Entity<BlogCategory>()
                .HasIndex(bc => bc.Slug)
                .IsUnique()
                .HasDatabaseName("IX_BlogCategories_Slug");

            modelBuilder.Entity<Blog>()
                .HasIndex(b => b.Slug)
                .IsUnique()
                .HasDatabaseName("IX_Blogs_Slug");

            // Configure composite unique constraint for active assignments
            modelBuilder.Entity<EngineerFarmerAssignment>()
                .HasIndex(efa => new { efa.EngineerId, efa.FarmerId, efa.IsActive })
                .IsUnique()
                .HasDatabaseName("IX_EngineerFarmerAssignments_Unique_Active")
                .HasFilter("[IsActive] = 1");


            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            // User-Engineer relationship (OK)
            modelBuilder.Entity<Engineer>()
                .HasOne(e => e.User)
                .WithOne(u => u.Engineer)
                .HasForeignKey<Engineer>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User-Farmer relationship (OK)
            modelBuilder.Entity<Farmer>()
                .HasOne(f => f.User)
                .WithOne(u => u.Farmer)
                .HasForeignKey<Farmer>(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // EngineerFarmerAssignment relationships (ĐÃ SỬA)
            modelBuilder.Entity<EngineerFarmerAssignment>()
                .HasOne(efa => efa.Engineer)
                .WithMany(e => e.EngineerFarmerAssignments)
                .HasForeignKey(efa => efa.EngineerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EngineerFarmerAssignment>()
                .HasOne(efa => efa.Farmer)
                .WithMany(f => f.EngineerFarmerAssignments)
                .HasForeignKey(efa => efa.FarmerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Blog relationships
            modelBuilder.Entity<Blog>()
                .HasOne(b => b.Author)
                .WithMany(u => u.Blogs)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict); // ✅ THAY ĐỔI: Cascade -> Restrict

            modelBuilder.Entity<Blog>()
                .HasOne(b => b.Category)
                .WithMany(bc => bc.Blogs)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ticket relationships (ĐÃ SỬA)
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Farmer)
                .WithMany(f => f.Tickets)
                .HasForeignKey(t => t.FarmerId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.AssignedEngineer)
                .WithMany(e => e.AssignedTickets)
                .HasForeignKey(t => t.AssignedEngineerId)
                .OnDelete(DeleteBehavior.SetNull); // OK - SetNull không gây conflict

            // TicketComment relationships (ĐÃ SỬA)
            modelBuilder.Entity<TicketComment>()
                .HasOne(tc => tc.Ticket)
                .WithMany(t => t.Comments)
                .HasForeignKey(tc => tc.TicketId)
                .OnDelete(DeleteBehavior.Cascade); // OK - chỉ có 1 path đến TicketComment

            modelBuilder.Entity<TicketComment>()
                .HasOne(tc => tc.User)
                .WithMany(u => u.TicketComments)
                .HasForeignKey(tc => tc.UserId)
                .OnDelete(DeleteBehavior.Restrict); // ✅ GIỮ NGUYÊN Restrict

            // Configure default values
            modelBuilder.Entity<Users>()
                .Property(u => u.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<Users>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Users>()
                .Property(u => u.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<BlogCategory>()
                .Property(bc => bc.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<EngineerFarmerAssignment>()
                .Property(efa => efa.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<Ticket>()
                .Property(t => t.Status)
                .HasDefaultValue("open");

            modelBuilder.Entity<Ticket>()
                .Property(t => t.Priority)
                .HasDefaultValue("medium");

            modelBuilder.Entity<Blog>()
                .Property(b => b.Status)
                .HasDefaultValue("draft");

            modelBuilder.Entity<Blog>()
                .Property(b => b.ViewCount)
                .HasDefaultValue(0);

            // Seed initial data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", Description = "System Administrator", CreatedAt = DateTime.UtcNow },
                new Role { Id = 2, Name = "Engineer", Description = "Agricultural Engineer", CreatedAt = DateTime.UtcNow },
                new Role { Id = 3, Name = "Farmer", Description = "Farmer User", CreatedAt = DateTime.UtcNow }
            );

            // Seed blog categories
            modelBuilder.Entity<BlogCategory>().HasData(
                new BlogCategory { Id = 1, Name = "Bệnh cây trồng", Description = "Các bài viết về bệnh hại trên cây trồng và cách phòng trị", Slug = "benh-cay-trong", IsActive = true, CreatedAt = DateTime.UtcNow },
                new BlogCategory { Id = 2, Name = "Kỹ thuật canh tác", Description = "Hướng dẫn kỹ thuật trồng trọt và chăm sóc cây", Slug = "ky-thuat-canh-tac", IsActive = true, CreatedAt = DateTime.UtcNow },
                new BlogCategory { Id = 3, Name = "Phân bón", Description = "Thông tin về các loại phân bón và cách sử dụng", Slug = "phan-bon", IsActive = true, CreatedAt = DateTime.UtcNow },
                new BlogCategory { Id = 4, Name = "Thuốc bảo vệ thực vật", Description = "Hướng dẫn sử dụng thuốc BVTV an toàn", Slug = "thuoc-bao-ve-thuc-vat", IsActive = true, CreatedAt = DateTime.UtcNow },
                new BlogCategory { Id = 5, Name = "Thời vụ", Description = "Lịch thời vụ và mùa vụ canh tác", Slug = "thoi-vu", IsActive = true, CreatedAt = DateTime.UtcNow }
            );

            // Seed default admin user (password should be hashed in real application)
            modelBuilder.Entity<Users>().HasData(
                new Users
                {
                    Id = 1,
                    UserName = "admin",
                    Password = "admin123", // This should be hashed
                    Email = "admin@agricultural.com",
                    Address = "System Address",
                    PhoneNumber = "0000000000",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );

            // Assign admin role to default user
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = 1, UserId = 1, RoleId = 1, CreatedAt = DateTime.UtcNow }
            );
        }
    }
}
