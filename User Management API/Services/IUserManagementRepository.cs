using User_Management_API.Entities;

namespace User_Management_API.Services;

public interface IUserManagmentRepository
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User?> GetUserByIdAsync(int userId);
    Task<User> CreateUserAsync(UserForCreation userForCreation);
    Task<User> UpdateUserAsync(int userId, UserForUpdate userForUpdate);
    Task DeleteUserAsync(int userId);

}