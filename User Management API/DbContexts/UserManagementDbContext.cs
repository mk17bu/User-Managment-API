using Microsoft.EntityFrameworkCore;
using User_Management_API.Entities;

namespace User_Management_API.DbContexts
{
    public class UserManagementDbContext(DbContextOptions<UserManagementDbContext> options) : DbContext(options)
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
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(u => u.LastName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(u => u.Mail)
                    .IsRequired()
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(p => p.Title)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(p => p.Content)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.HasOne(p => p.User)
                    .WithMany(u => u.Posts)
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(p => p.Content)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(c => c.User)
                    .WithMany(u => u.Comments)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(c => c.PostId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Reaction>(entity =>
            {
                entity.HasOne(r => r.User)
                    .WithMany(u => u.Reactions)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Post)
                    .WithMany(p => p.Reactions)
                    .HasForeignKey(r => r.PostId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Marc-André", LastName = "ter Stegen", Mail = "tersteguen@fcb.com"},
                new User { Id = 2, FirstName = "Andreas", LastName = "Christensen", Mail = "andreaschristensen@fcb.com"},
                new User { Id = 3, FirstName = "Jules", LastName = "Koundé", Mail = "juleskounde@fcb.com"},
                new User { Id = 4, FirstName = "Joshua", LastName = "Kimmich", Mail = "joshuakimmich@fcb.com"},
                new User { Id = 5, FirstName = "Robert", LastName = "Lewandoski", Mail = "robertlewandoski@fcb.com"}
            );
            
            modelBuilder.Entity<Post>().HasData(
                new Post 
                { 
                    Id = 1, 
                    UserId = 1,
                    Title = "Welcome Joshua Kimmich!", 
                    Content = "Joshua Kimmich has officially joined FC Barcelona.",
                    Date = DateTime.UtcNow.AddHours(-2),
                },
                new Post 
                {
                    Id = 2, 
                    UserId = 4,
                    Title = "Hello team!",
                    Content = "I'm very happy to be here and I'll try my best to fit the team.",
                    Date = DateTime.UtcNow.AddHours(-1),
                }
            );
            
            modelBuilder.Entity<Comment>().HasData(
                new Comment
                {
                    Id = 1,
                    UserId = 2,
                    PostId = 1,
                    Content = "Nicee",
                    Date = DateTime.UtcNow.AddHours(-2),
                },
                new Comment
                {
                    Id = 2,
                    UserId = 3,
                    PostId = 2,
                    Content = "I'll teach you how to defend!",
                    Date = DateTime.UtcNow.AddHours(-1),
                },
                new Comment
                {
                    Id = 3,
                    UserId = 5,
                    PostId = 2,
                    Content = "Let's gooo!",
                    Date = DateTime.UtcNow,
                }
            );
            
            modelBuilder.Entity<Reaction>().HasData(
                new Reaction
                {
                    Id = 1,
                    UserId = 2,
                    PostId = 1,
                    Type = ReactionType.Like,
                },
                new Reaction
                {
                    Id = 2,
                    UserId = 3,
                    PostId = 2,
                    Type = ReactionType.Like,
                },
                new Reaction
                {
                    Id = 3,
                    UserId = 5,
                    PostId = 2,
                    Type = ReactionType.Heart,
                }
            );
            
            base.OnModelCreating(modelBuilder);
        }
    }
}