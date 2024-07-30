using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Sora.TodoList.BL.Users;
using Sora.TodoList.BL.Users.Dtos;
using System;
using System.Threading.Tasks;

namespace Sora.TodoList.HttpApi.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Khởi tạo

        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetService<ILogger<UserController>>() ?? NullLogger<UserController>.Instance;
            _userService = serviceProvider.GetRequiredService<IUserService>();
        }

        #endregion Khởi tạo

        #region Login user

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginReqDto payload)
        {
            var res = await _userService.Login(payload);
            return Ok(res);
        }

        #endregion Login user
    }
}