using Microsoft.AspNetCore.Mvc;
using SaleApi.Models;
using SaleApi.Services;

namespace SaleApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUser()
        {
            var user = await _userService.GetAllUser();
            return Ok(user);
        }
    }
}
