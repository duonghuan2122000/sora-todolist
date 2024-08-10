using System;

namespace Sora.TodoList.DL.Data.Entities
{
    public class NotificationEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Id tenant
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// Id user
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// tiêu đề
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// nội dung
        /// </summary>
        public string Content { get; set; }

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