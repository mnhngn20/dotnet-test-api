using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class UserRepository : IUserRepository
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UserRepository(DataContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<MemberDto> GetMemberAsync(int id)
    {
      return await _context.Users
        .Where(user => user.Id == id)
        .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
        .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<MemberDto>> GetMembersAsync()
    {
      return await _context.Users.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
      return await _context.Users.Include(user => user.Photos).SingleOrDefaultAsync(user => user.Id == id);
    }

    public async Task<User> GetUserByUserNameAsync(string userName)
    {
      return await _context.Users.Include(user => user.Photos).SingleOrDefaultAsync(user => user.UserName == userName.ToLower());
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
      return await _context.Users.Include(user => user.Photos).ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

    public void Update(User user)
    {
      _context.Entry(user).State = EntityState.Modified;
    }
  }

};

