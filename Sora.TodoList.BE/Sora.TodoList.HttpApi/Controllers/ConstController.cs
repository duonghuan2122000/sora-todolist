using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Sora.TodoList.BL.ConstItems;
using System;
using System.Threading.Tasks;

namespace Sora.TodoList.HttpApi.Controllers
{
    [Route("api/Consts")]
    [ApiController]
    public class ConstController : TodoListControllerBase
    {
        #region Khởi tạo

        private readonly IConstService _constService;

        public ConstController(IServiceProvider serviceProvider)
        {
            _constService = serviceProvider.GetRequiredService<IConstService>();
        }

        #endregion Khởi tạo

        #region Hàm

        /// <summary>
        /// Lấy danh sách const
        /// </summary>
        /// <param name="constListKey"></param>
        /// <returns></returns>
        [HttpGet("{constListKey}")]
        public async Task<IActionResult> GetConstList(string constListKey)
        {
            var result = await _constService.GetConstList(constListKey);
            return Ok(result);
        }

        #endregion Hàm
    }
}