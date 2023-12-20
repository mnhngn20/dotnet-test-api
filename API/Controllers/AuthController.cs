using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  public class AuthController : BaseAPIController
  {
    public ITokenService _tokenService;
    public DataContext _context;
    public AuthController(DataContext context, ITokenService tokenService)
    {
      _context = context;
      _tokenService = tokenService;

    }

    [HttpPost("register")] // POST: api/auth/register
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {

      using var hmac = new HMACSHA512();

      if (await UserExists(registerDto.UserName))
      {
        return BadRequest("Username is exists!");
      }

      var newUser = new User
      {
        UserName = registerDto.UserName.ToLower(),
        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
        PasswordSalt = hmac.Key
      };

      _context.Users.Add(newUser);

      await _context.SaveChangesAsync();

      return new UserDto
      {
        Token = _tokenService.CreateToken(newUser),
        UserName = newUser.UserName,
      };
    }

    [HttpPost("login")] // POST: api/auth/login
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {

      var user = await GetUserByUserName(loginDto.UserName);

      if (user == null)
      {
        return Unauthorized("User not found!");
      }

      using var hmac = new HMACSHA512(user.PasswordSalt);

      var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

      for (int i = 0; i < computedHash.Length; i++)
      {
        if (computedHash[i] != user.PasswordHash[i])
        {
          return Unauthorized("Password does not match!");
        };
      }

      return new UserDto
      {
        Token = _tokenService.CreateToken(user),
        UserName = user.UserName
      };
    }

    private async Task<User> GetUserByUserName(string userName)
    {
      return await _context.Users.SingleOrDefaultAsync(user => user.UserName == userName.ToLower());
    }

    private async Task<bool> UserExists(string username)
    {
      return await _context.Users.AnyAsync(user => user.UserName == username.ToLower());
    }
  }
}