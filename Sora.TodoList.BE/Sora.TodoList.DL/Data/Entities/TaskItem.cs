using System;

namespace Sora.TodoList.DL.Data.Entities
{
    /// <summary>
    /// Thông tin task
    /// </summary>
    public class TaskItem
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id tenant
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// Id user tạo task
        /// </summary>
        public string UserIdCreated { get; set; }

        /// <summary>
        /// Id user được gắn task
        /// </summary>
        public string UserIdAssign { get; set; }

        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Nội dung
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Trang thái
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Thời gian cập nhật
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}