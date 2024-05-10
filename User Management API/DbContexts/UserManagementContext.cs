using Microsoft.EntityFrameworkCore;
using User_Management_API.Entities;

namespace User_Management_API.DbContexts;

public class UserManagementContext(DbContextOptions<UserManagementContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, FirstName = "Marc-André", LastName = "ter Stegen", Mail = "tersteguen@fcb.com", Roles = new List<Role> { Role.Goalkeeper}},
            new User { Id = 2, FirstName = "Andreas", LastName = "Christensen", Mail = "andreaschristensen@fcb.com", Roles = new List<Role> { Role.CenterBack, Role.DefensiveMidfielder}},
            new User { Id = 3, FirstName = "Jules", LastName = "Koundé", Mail = "juleskounde@fcb.com", Roles = new List<Role> { Role.CenterBack, Role.FullBack}},
            new User { Id = 4, FirstName = "Joshua", LastName = "Kimmich", Mail = "joshuakimmich@fcb.com", Roles = new List<Role> { Role.DefensiveMidfielder, Role.FullBack}},
            new User { Id = 5, FirstName = "Robert", LastName = "Lewandoski", Mail = "robertlewandoski@fcb.com", Roles = new List<Role> { Role.Striker}}
        );
    }
}