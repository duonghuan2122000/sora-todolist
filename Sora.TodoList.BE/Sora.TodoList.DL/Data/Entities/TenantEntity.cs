using System;

namespace Sora.TodoList.DL.Data.Entities
{
    /// <summary>
    /// Thông tin tenant
    /// </summary>
    public class TenantEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Tên
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Thông tin bổ sung, mặc định: {}
        /// </summary>
        public string ExtraProperties { get; set; }

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