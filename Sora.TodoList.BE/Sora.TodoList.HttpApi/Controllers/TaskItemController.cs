using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Sora.TodoList.BL.TaskItems;
using Sora.TodoList.BL.TaskItems.Dtos;
using System;
using System.Threading.Tasks;

namespace Sora.TodoList.HttpApi.Controllers
{
    [Route("api/Tasks")]
    [ApiController]
    [Authorize]
    public class TaskItemController : TodoListControllerBase
    {
        #region Khởi tạo

        private readonly ITaskItemService _taskItemService;

        public TaskItemController(IServiceProvider serviceProvider)
        {
            _taskItemService = serviceProvider.GetRequiredService<ITaskItemService>();
        }

        #endregion Khởi tạo

        #region Hàm lấy danh sách bản ghi

        /// <summary>
        /// Lấy danh sách bản ghi
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("List")]
        public async Task<IActionResult> GetList([FromBody] GetListTaskItemReqDto payload)
        {
            var result = await _taskItemService.GetList(payload);
            return Ok(result);
        }

        #endregion Hàm lấy danh sách bản ghi

        #region Hàm lấy thông tin công việc

        /// <summary>
        /// Lấy thông tin công việc
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var taskItem = await _taskItemService.Get(id);
            return Ok(taskItem);
        }

        #endregion Hàm lấy thông tin công việc
    }
}