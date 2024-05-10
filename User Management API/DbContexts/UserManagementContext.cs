using Microsoft.EntityFrameworkCore;
using User_Management_API.Entities;

namespace User_Management_API.DbContexts
{
    public class UserManagementContext(DbContextOptions<UserManagementContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Marc-André", LastName = "ter Stegen", Mail = "tersteguen@fcb.com", Role = Role.Goalkeeper},
                new User { Id = 2, FirstName = "Andreas", LastName = "Christensen", Mail = "andreaschristensen@fcb.com", Role = Role.CenterBack},
                new User { Id = 3, FirstName = "Jules", LastName = "Koundé", Mail = "juleskounde@fcb.com", Role = Role.FullBack},
                new User { Id = 4, FirstName = "Joshua", LastName = "Kimmich", Mail = "joshuakimmich@fcb.com", Role = Role.CentralMidfielder},
                new User { Id = 5, FirstName = "Robert", LastName = "Lewandoski", Mail = "robertlewandoski@fcb.com", Role = Role.Striker}
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}