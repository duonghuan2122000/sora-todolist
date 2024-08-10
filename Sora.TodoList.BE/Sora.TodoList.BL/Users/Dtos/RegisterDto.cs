namespace Sora.TodoList.BL.Users.Dtos
{
    /// <summary>
    /// Dto req đăng ký
    /// </summary>
    public class RegisterReqDto
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

    public class RegisterResDto : LoginResDto
    {
    }
}