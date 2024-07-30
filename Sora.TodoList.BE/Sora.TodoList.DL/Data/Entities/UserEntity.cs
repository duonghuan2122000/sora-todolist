using System;

namespace Sora.TodoList.DL.Data.Entities
{
    /// <summary>
    /// Thông tin user
    /// </summary>
    public class UserEntity
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
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Salt mật khẩu
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Mật khẩu đã hash
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Tên
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Họ và tên đệm
        /// </summary>
        public string LastName { get; set; }

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