using Newtonsoft.Json;
using Sora.TodoList.DL.Data.Etos;
using System;
using System.Collections.Generic;

namespace Sora.TodoList.BL.TaskItems.Dtos
{
    /// <summary>
    /// Dto req lấy danh sách công việc
    /// </summary>
    public class GetListTaskItemReqDto : GetListTaskItemEto
    {
    }

    /// <summary>
    /// Dto res lấy danh sách công việc
    /// </summary>
    public class GetListTaskItemResDto
    {
        /// <summary>
        /// Dto công việc
        /// </summary>
        public class TaskItemDto
        {
            /// <summary>
            /// Id
            /// </summary>
            public string Id { get; set; }

            /// <summary>
            /// Tiêu đề
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// Nội dung
            /// </summary>
            public string Content { get; set; }

            /// <summary>
            /// Trạng thái
            /// </summary>
            public string Status { get; set; }

            /// <summary>
            /// Trạng thái dạng text
            /// </summary>
            public string StatusText { get; set; }

            [JsonIgnore]
            public string UserIdCreated { get; set; }

            public UserDto UserCreated { get; set; }

            [JsonIgnore]
            public string UserIdAssign { get; set; }

            public UserDto UserAssign { get; set; }
        }

        public class UserDto
        {
            public string Id { get; set; }

            public string Email { get; set; }
        }

        /// <summary>
        /// Tổng bản ghi
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Danh sách
        /// </summary>
        public List<TaskItemDto> Items { get; set; } = new List<TaskItemDto>();
    }

    public class TaskItemDto
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Nội dung
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Lần cập nhật cuối
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }

    public class CreateTaskItemDto
    {
        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Nội dung
        /// </summary>
        public string Content { get; set; }

        public string Status { get; set; }
    }
}