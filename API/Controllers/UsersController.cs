using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController : BaseAPIController
{
  private readonly IUserRepository _userRepository;
  private readonly IMapper _mapper;

  public UsersController(IUserRepository userRepository, IMapper mapper)
  {
    _userRepository = userRepository;
    _mapper = mapper;
  }

  [Authorize]
  [HttpGet] // api/users
  public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
  {
    var users = await _userRepository.GetMembersAsync();

    var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);

    return Ok(usersToReturn);
  }

  [HttpGet("{id}")] // api/users/:id
  public async Task<ActionResult<MemberDto>> GetUser(int id)
  {
    var user = await _userRepository.GetUserByIdAsync(id);

    var userToReturn = _mapper.Map<MemberDto>(user);

    return userToReturn;
  }
}