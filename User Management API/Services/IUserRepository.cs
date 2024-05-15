using User_Management_API.Entities;
using User_Management_API.Models;

namespace User_Management_API.Services;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User?> GetUserByIdAsync(int userId);
    Task<User> CreateUserAsync(UserForCreationDto userForCreationDto);
    Task<User> UpdateUserAsync(int userId, UserForUpdateDto userForUpdateDto);
    Task DeleteUserAsync(int userId);

}