using User_Management_API.DbContexts;
using User_Management_API.Entities;

namespace User_Management_API.Services;

public class UserManagementRepository(UserManagementContext context)
{
    public List<User> GetAllUsers()
    {
        return context.Users.ToList();
    }

    public User? GetUserById(int userId)
    {
        return context.Users.FirstOrDefault(u => u.Id == userId);
    }

    public User CreateUser(UserForCreation userForCreation)
    {
        var newUser = new User
        {
            FirstName = userForCreation.FirstName,
            LastName = userForCreation.LastName,
            Mail = userForCreation.Mail,
            Roles = userForCreation.Roles
        };

        context.Users.Add(newUser);
        context.SaveChanges();

        return newUser;
    }

    public User UpdateUser(int userId, UserForUpdate userForUpdate)
    {
        var existingUser = context.Users.Find(userId);
        if (existingUser == null)
        {
            throw new ArgumentException("User not found");
        }

        existingUser.UpdateUser(userForUpdate.FirstName, userForUpdate.LastName, userForUpdate.Mail, userForUpdate.Roles);

        context.SaveChanges();

        return existingUser;
    }

    public void DeleteUser(int userId)
    {
        var user = context.Users.Find(userId);
        if (user == null) return;
        context.Users.Remove(user);
        context.SaveChanges();
    }
}