using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")] // /api/users
public class UsersController: ControllerBase {
    private readonly DataContext _context;

    public UsersController(DataContext context) {
        this._context = context;
    } 

    [HttpGet] // api/users
    public async Task<ActionResult<IEnumerable<User>>> GetUsers() {
      var users = await _context.Users.ToListAsync();

      return users;
    }

    [HttpGet("{id}")] // api/users/:id
    public async Task<ActionResult<User>> getUser(int id) {
      var user = await _context.Users.FindAsync(id);

      return user;
    }
}