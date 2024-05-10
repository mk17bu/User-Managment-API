using Microsoft.AspNetCore.Mvc;
using User_Management_API.Entities;
using User_Management_API.Services;

namespace User_Management_API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(UserManagementRepository userManagementRepository) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = userManagementRepository.GetAllUsers();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        var user = userManagementRepository.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public ActionResult<User> CreateUser(UserForCreation userForCreation)
    {
        var newUser = userManagementRepository.CreateUser(userForCreation);
        return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
    }

    [HttpPut("{userId}")]
    public ActionResult UpdateUser(int userId, UserForUpdate userForUpdate)
    {
        userManagementRepository.UpdateUser(userId, userForUpdate);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        userManagementRepository.DeleteUser(id);
        return NoContent();
    }
}