using Microsoft.AspNetCore.Mvc;
using User_Management_API.Models;
using User_Management_API.Services;

namespace User_Management_API.Controller;

[ApiController]
[Route("api/users")]
public class UsersController(UserManagmentRepository userManagmentRepository) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = userManagmentRepository.GetAllUsers();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        var user = userManagmentRepository.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public ActionResult<User> CreateUser(UserForCreation userForCreation)
    {
        var newUser = userManagmentRepository.CreateUser(userForCreation);
        return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
    }

    [HttpPut("{userId}")]
    public ActionResult UpdateUser(int userId, UserForUpdate userForUpdate)
    {
        userManagmentRepository.UpdateUser(userId, userForUpdate);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        userManagmentRepository.DeleteUser(id);
        return NoContent();
    }
}