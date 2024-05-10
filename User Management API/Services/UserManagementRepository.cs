using Microsoft.EntityFrameworkCore;
using User_Management_API.DbContexts;
using User_Management_API.Entities;

namespace User_Management_API.Services;

public class UserManagementRepository(UserManagementContext context) : IUserManagmentRepository
{
    private readonly UserManagementContext _context = context ?? throw new ArgumentNullException(nameof(context));
    
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.Users.OrderBy(u => u.FirstName).ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }
    
    public async Task<User> CreateUserAsync(UserForCreation userForCreation)
    {
        var newUser = new User
        {
            FirstName = userForCreation.FirstName,
            LastName = userForCreation.LastName,
            Mail = userForCreation.Mail,
            Role = userForCreation.Role
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return newUser;
    }

    public async Task<User> UpdateUserAsync(int userId, UserForUpdate userForUpdate)
    {
        var existingUser = await _context.Users.FindAsync(userId);
        if (existingUser == null)
        {
            throw new ArgumentException("User not found");
        }

        existingUser.UpdateUser(userForUpdate.FirstName, userForUpdate.LastName, userForUpdate.Mail, userForUpdate.Role);
        await _context.SaveChangesAsync();

        return existingUser;
    }

    public async Task DeleteUserAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}