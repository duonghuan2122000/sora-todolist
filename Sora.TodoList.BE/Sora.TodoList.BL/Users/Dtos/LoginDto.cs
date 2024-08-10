namespace Sora.TodoList.BL.Users.Dtos
{
    /// <summary>
    /// Dto req login
    /// </summary>
    public class LoginReqDto
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string Password { get; set; }
    }

    /// <summary>
    /// Dto res login
    /// </summary>
    public class LoginResDto
    {
        /// <summary>
        /// AccessToken
        /// </summary>
        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }

        public string RefreshToken { get; set; }
    }
}