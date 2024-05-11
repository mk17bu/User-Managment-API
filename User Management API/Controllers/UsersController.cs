using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User_Management_API.Entities;
using User_Management_API.Models;
using User_Management_API.Services;

namespace User_Management_API.Controllers;

[ApiController]
[Authorize]
[Route("api/users")]
public class UsersController(IUserRepository userRepository) : ControllerBase
{
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userRepository.GetUsersAsync();
        return Ok(users);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(UserForCreationDto userForCreationDto)
    {
        var newUser = await _userRepository.CreateUserAsync(userForCreationDto);
        return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
    }
    
    [HttpPut("{userId}")]
    public async Task<ActionResult> UpdateUser(int userId, UserForUpdateDto userForUpdateDto)
    {
        await _userRepository.UpdateUserAsync(userId, userForUpdateDto);
        return NoContent();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        await _userRepository.DeleteUserAsync(userId);
        return NoContent();
    }
}