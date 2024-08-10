using Sora.TodoList.DL.Commons.Consts;
using System;

namespace Sora.TodoList.DL.Data.Entities
{
    /// <summary>
    /// Thông tin token
    /// </summary>
    public class TokenEntity
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
        /// Loại token
        /// - REFRESH_TOKEN
        /// </summary>
        public string TokenType { get; set; } = CommonConst.TokenType.RefreshToken;

        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Thời gian hết hạn token
        /// </summary>
        public DateTime ExpiredDate { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Thời gian cập nhật
        /// </summary>
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}