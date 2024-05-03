using Microsoft.AspNetCore.Mvc;
using User_Management_API.Models;
using User_Management_API.Services;

namespace User_Management_API.Controller;

[ApiController]
[Route("api/users")]
public class UserController(UserServices userService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = userService.GetAllUsers();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        var user = userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public ActionResult<User> CreateUser(UserForCreation userForCreation)
    {
        var newUser = userService.CreateUser(userForCreation);
        return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
    }

    [HttpPut("{userId}")]
    public ActionResult UpdateUser(int userId, UserForUpdate userForUpdate)
    {
        userService.UpdateUser(userId, userForUpdate);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        userService.DeleteUser(id);
        return NoContent();
    }
}