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

    }

    [HttpGet]
    public ActionResult<UserDto> GetUser(string mail)
    {

    }
}