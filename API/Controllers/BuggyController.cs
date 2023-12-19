using API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class BuggyController : BaseAPIController
  {
    private readonly DataContext _context;
    public BuggyController(DataContext context)
    {
      _context = context;
    }

    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetSecret()
    {
      return "secret text";
    }
  }
};

