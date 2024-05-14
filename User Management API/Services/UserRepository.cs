using Microsoft.EntityFrameworkCore;
using User_Management_API.DbContexts;
using User_Management_API.Entities;
using User_Management_API.Models;

namespace User_Management_API.Services;

public class UserRepository(UserManagementDbContext context) : IUserRepository
{
    private readonly UserManagementDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.Users.OrderBy(u => u.FirstName).ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }
    
    public async Task<User> CreateUserAsync(UserForCreationDto userForCreationDto)
    {
        var newUser = new User
        {
            FirstName = userForCreationDto.FirstName,
            LastName = userForCreationDto.LastName,
            Mail = userForCreationDto.Mail,
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return newUser;
    }

    public async Task<User> UpdateUserAsync(int userId, UserForUpdateDto userForUpdateDto)
    {
        var existingUser = await _context.Users.FindAsync(userId);
        if (existingUser == null)
        {
            throw new ArgumentException("User not found");
        }

        existingUser.UpdateUser(userForUpdateDto.FirstName, userForUpdateDto.LastName, userForUpdateDto.Mail);
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