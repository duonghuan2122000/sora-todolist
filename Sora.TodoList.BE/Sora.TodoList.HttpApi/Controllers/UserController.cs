using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Sora.TodoList.BL.Users;
using Sora.TodoList.BL.Users.Dtos;
using System;
using System.Security.Claims;
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

        #region Hàm đăng ký

        /// <summary>
        /// Đăng ký
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterReqDto payload)
        {
            var res = await _userService.Register(payload);
            return Ok(res);
        }

        #endregion Hàm đăng ký

        #region Hàm lấy thông tin user

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            var user = HttpContext.User;
            return Ok(new
            {
                Id = user.FindFirstValue(ClaimTypes.Sid)
            });
        }

        #endregion Hàm lấy thông tin user
    }
}