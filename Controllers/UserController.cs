using DotNetCRUDWebAPIs.DTOs;
using DotNetCRUDWebAPIs.Models;
using DotNetCRUDWebAPIs.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCRUDWebAPIs.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _user;

    public UserController(IUserRepository user)
    {
        _user = user;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _user.GetUsers();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById([FromRoute] long id)
    {
        var user = await _user.GetUserById(id);

        if (user == null)
            return NotFound("User not found");

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser([FromBody] UserCreateDTO user)
    {
        var toCreate = new User
        {
            Username = user.Username,
            FullName = user.FullName,
            Email = user.Email,
            Password = user.Password,
            Phone = user.Phone
        };

        var createdUser = await _user.CreateUser(toCreate);

        if (createdUser == null)
            return BadRequest("User creation failed.");

        return Ok(createdUser);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser([FromRoute] long id, [FromBody] UserUpdateDTO user)
    {
        var existingUser = await _user.GetUserById(id);

        if (existingUser == null)
            return NotFound("User not found.");

        var toUpdate = existingUser with
        {
            Username = user.Username ?? existingUser.Username,
            FullName = user.FullName ?? existingUser.FullName,
            Email = user.Email ?? existingUser.Email,
            Password = user.Password ?? existingUser.Password,
            Phone = user.Phone ?? existingUser.Phone
        };

        var didUpdated = await _user.UpdateUser(toUpdate);

        if (!didUpdated)
            return BadRequest("User updation failed.");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser([FromRoute] long id)
    {
        var existingUser = await _user.GetUserById(id);

        if (existingUser == null)
            return NotFound("User not found");

        var didDeleted = await _user.DeleteUser(existingUser.Id);

        if (!didDeleted)
            return BadRequest("User deletion failed.");

        return NoContent();
    }
}

