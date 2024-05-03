using Microsoft.AspNetCore.Mvc;
using User_Management_API.Data;
using User_Management_API.Models;

namespace User_Management_API.Controller;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        var users = _context.Users;
        return Ok(users);
    }

    [HttpGet("{mail}")]
    public ActionResult<User> GetUser(string mail)
    {
        var userToReturn = _context.Users.FirstOrDefault(u => u.Mail == mail);
        if (userToReturn == null)
        {
            return NotFound();
        }

        return Ok(userToReturn);
    }

    [HttpPost]
    public ActionResult<User> CreateUser(UserForCreation userForCreation)
    {
        var highestId = _context.Users.Max(u => u.Id);

        var newUser = new User()
        {
            Id = ++highestId,
            FirstName = userForCreation.FirstName,
            LastName = userForCreation.LastName,
            Mail = userForCreation.Mail,
            Roles = userForCreation.Roles
        };

        _context.Users.Add(newUser);

        return CreatedAtRoute("GetUserById", new { id = newUser.Id }, newUser);
    }

    [HttpPut("{userId}")]
    public ActionResult UpdateUser(int userId, UserForUpdate userForUpdate)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
        {
            return NotFound();
        }

        user.FirstName = userForUpdate.FirstName;
        user.LastName = userForUpdate.LastName;
        user.Mail = userForUpdate.Mail;
        user.Roles = userForUpdate.Roles;

        return NoContent();
    }

    [HttpDelete("{userId}")]
    public ActionResult DeleteUser(int userId)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        return NoContent();
    }
}