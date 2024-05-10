using Microsoft.EntityFrameworkCore;
using User_Management_API.Entities;

namespace User_Management_API.DbContexts
{
    public class UserManagementContext(DbContextOptions<UserManagementContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.FirstName)
                    .IsRequired().HasMaxLength(20);

                entity.Property(u => u.Mail)
                    .IsRequired()
                    .HasMaxLength(40);
            });
            
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Marc-André", LastName = "ter Stegen", Mail = "tersteguen@fcb.com"},
                new User { Id = 2, FirstName = "Andreas", LastName = "Christensen", Mail = "andreaschristensen@fcb.com"},
                new User { Id = 3, FirstName = "Jules", LastName = "Koundé", Mail = "juleskounde@fcb.com"},
                new User { Id = 4, FirstName = "Joshua", LastName = "Kimmich", Mail = "joshuakimmich@fcb.com"},
                new User { Id = 5, FirstName = "Robert", LastName = "Lewandoski", Mail = "robertlewandoski@fcb.com"}
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}