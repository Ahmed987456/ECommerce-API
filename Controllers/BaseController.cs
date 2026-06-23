using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected int GetCurrentUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        protected string GetCurrentUserRole()
        {
            return User.FindFirst(ClaimTypes.Role)?.Value;
        }
    }
}