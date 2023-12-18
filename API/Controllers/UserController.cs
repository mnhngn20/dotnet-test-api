using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class UserController : BaseAPIController {
    private readonly DataContext _context;

    public UserController(DataContext context) {
        this._context = context;
    } 

[Authorize] 
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