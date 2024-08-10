using Microsoft.Extensions.DependencyInjection;
using Sora.TodoList.BL.TaskItems.Dtos;
using Sora.TodoList.DL.Data.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sora.TodoList.BL.TaskItems
{
    public interface ITaskItemService
    {
        /// <summary>
        /// Lấy danh sách bản ghi
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        Task<GetListTaskItemResDto> GetList(GetListTaskItemReqDto payload);

        /// <summary>
        /// Lấy thông tin công việc
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TaskItemDto> Get(string id);
    }

    public class TaskItemService : ITaskItemService
    {
        #region Khởi tạo

        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IConstRepository _constRepository;
        private readonly IUserRepository _userRepository;

        public TaskItemService(IServiceProvider serviceProvider)
        {
            _taskItemRepository = serviceProvider.GetRequiredService<ITaskItemRepository>();
            _constRepository = serviceProvider.GetRequiredService<IConstRepository>();
            _userRepository = serviceProvider.GetRequiredService<IUserRepository>();
        }

        #endregion Khởi tạo

        #region Hàm lấy danh sách bản ghi

        /// <summary>
        /// Lấy danh sách bản ghi
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public async Task<GetListTaskItemResDto> GetList(GetListTaskItemReqDto payload)
        {
            var result = new GetListTaskItemResDto
            {
                TotalCount = await _taskItemRepository.Count(payload)
            };
            if (result.TotalCount > 0)
            {
                result.Items = await _taskItemRepository.GetList<GetListTaskItemResDto.TaskItemDto>(payload);

                var statusListTask = _constRepository.GetConstList("TASKITEM_STATUS");
                var usersTask = _userRepository.GetList<GetListTaskItemResDto.UserDto>([.. result.Items.Select(x => x.UserIdCreated), .. result.Items.Select(x => x.UserIdAssign)]);

                await Task.WhenAll(statusListTask, usersTask);

                var statusList = statusListTask.Result;
                var users = usersTask.Result;

                result.Items.ForEach(item =>
                {
                    item.StatusText = statusList.FirstOrDefault(x => x.Key == item.Status)?.Value ?? string.Empty;
                    item.UserCreated = users.FirstOrDefault(x => x.Id == item.UserIdCreated);
                    item.UserAssign = users.FirstOrDefault(x => x.Id == item.UserIdAssign);
                });
            }
            return result;
        }

        #endregion Hàm lấy danh sách bản ghi

        #region Hàm lấy thông tin công việc

        /// <summary>
        /// Lấy thông tin công việc
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TaskItemDto> Get(string id)
        {
            var taskItem = await _taskItemRepository.Get<TaskItemDto>(id);
            return taskItem;
        }

        #endregion Hàm lấy thông tin công việc
    }
}