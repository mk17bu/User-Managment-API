using Microsoft.AspNetCore.Mvc;
using User_Management_API.Models;

namespace User_Management_API.Controller;

[ApiController]
[Route("api/users")]
public class UserController(UsersDataStore usersDataStore) : ControllerBase
{
    private readonly UsersDataStore _usersDataStore =
        usersDataStore ?? throw new ArgumentNullException(nameof(usersDataStore));

    [HttpGet]
    public ActionResult<IEnumerable<UserDto>> GetUsers()
    {
        return Ok(_usersDataStore.Users);
    }

    [HttpGet("{mail}")]
    public ActionResult<UserDto> GetUser(string mail)
    {
        var userToReturn = _usersDataStore.Users.FirstOrDefault(u => u.Mail == "mail");
        if (userToReturn == null)
        {
            return NotFound();
        }

        return Ok(userToReturn);
    }

    [HttpPost]
    public ActionResult<UserDto> CreateUser(UserForCreationDto userForCreation)
    {
        var highestId = _usersDataStore.Users.Max(u => u.Id);

        var newUser = new UserDto()
        {
            Id = ++highestId,
            FirstName = userForCreation.FirstName,
            LastName = userForCreation.LastName,
            Mail = userForCreation.Mail
        };

        _usersDataStore.Users.Add(newUser);

        return CreatedAtRoute("GetUserById", new { id = newUser.Id }, newUser);
    }

    [HttpPut("{userId}")]
    public ActionResult UpdateUser(int userId, UserForUpdateDto userForUpdate)
    {
        var user = _usersDataStore.Users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
        {
            return NotFound();
        }

        user.FirstName = userForUpdate.FirstName;
        user.LastName = userForUpdate.LastName;
        user.Mail = userForUpdate.Mail;

        return NoContent();
    }
}