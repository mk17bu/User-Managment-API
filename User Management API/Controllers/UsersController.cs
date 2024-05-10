using Microsoft.AspNetCore.Mvc;
using User_Management_API.Entities;
using User_Management_API.Services;

namespace User_Management_API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IUserManagmentRepository userManagementRepository) : ControllerBase
{
    private readonly IUserManagmentRepository _userManagementRepository = userManagementRepository ?? throw new ArgumentNullException(nameof(userManagementRepository));
    
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userManagementRepository.GetUsersAsync();
        return Ok(users);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(int userId)
    {
        var user = await _userManagementRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(UserForCreation userForCreation)
    {
        var newUser = await _userManagementRepository.CreateUserAsync(userForCreation);
        return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
    }
    
    [HttpPut("{userId}")]
    public async Task<ActionResult> UpdateUser(int userId, UserForUpdate userForUpdate)
    {
        await _userManagementRepository.UpdateUserAsync(userId, userForUpdate);
        return NoContent();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        await _userManagementRepository.DeleteUserAsync(userId);
        return NoContent();
    }
}