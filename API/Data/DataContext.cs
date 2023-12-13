namespace API.Data;

using API.Entities;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext {
  public DataContext(DbContextOptions options): base(options) {} 

  public DbSet<User> Users { get; set; }
}