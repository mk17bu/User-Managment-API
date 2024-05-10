using Microsoft.AspNetCore.Mvc;
using User_Management_API.Entities;
using User_Management_API.Services;

namespace User_Management_API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(UserManagementRepository userManagementRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await userManagementRepository.GetUsersAsync();
        return Ok(users);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(int userId)
    {
        var user = await userManagementRepository.GetUserByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(UserForCreation userForCreation)
    {
        var newUser = await userManagementRepository.CreateUserAsync(userForCreation);
        return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
    }
    
    [HttpPut("{userId}")]
    public async Task<ActionResult> UpdateUser(int userId, UserForUpdate userForUpdate)
    {
        await userManagementRepository.UpdateUserAsync(userId, userForUpdate);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await userManagementRepository.DeleteUserAsync(id);
        return NoContent();
    }
}