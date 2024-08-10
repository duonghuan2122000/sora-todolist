namespace Sora.TodoList.DL.Commons.Consts
{
    public static class CommonConst
    {
        /// <summary>
        /// Trạng thái task
        /// </summary>
        public static class TaskItemStatus
        {
            /// <summary>
            /// Task được đưa vào kế hoạch
            /// </summary>
            public const string Backlog = "BACKLOG";

            /// <summary>
            /// Task đang hoạt động
            /// </summary>
            public const string Ready = "READY";

            /// <summary>
            /// Task được trong được thực hiện
            /// </summary>
            public const string InProgress = "IN_PROGRESS";

            /// <summary>
            /// Task đang được phê duyệt
            /// </summary>
            public const string InReview = "IN_REVIEW";

            /// <summary>
            /// Task hoàn thành
            /// </summary>
            public const string Done = "DONE";
        }

        public static class ErrorInfo
        {
            public static class Login
            {
                public static class Code
                {
                    public const string EmailInvalid = "LOGIN_001";

                    public const string PasswordInvalid = "LOGIN_002";
                }

                public static class Message
                {
                    public const string EmailInvalid = "Email không tồn tại hoặc không hợp lệ";

                    public const string PasswordInvalid = "Mật khẩu không hợp lệ";
                }
            }

            public static class Register
            {
                public static class Code
                {
                    public const string EmailExist = "REGISTER_001";

                    public const string PasswordInvalid = "REGISTER_002";
                }

                public static class Message
                {
                    public const string EmailExist = "Email đã tồn tại";

                    public const string PasswordInvalid = "Mật khẩu không hợp lệ (mật khẩu cần ít nhất 6 ký tự)";
                }
            }
        }

        public static class TokenType
        {
            public const string RefreshToken = "REFRESH_TOKEN";
        }

        public static class TaskItem
        {
            public static class Status
            {
                // lưu nháp
                public const string DRAFT = "DRAFT";

                // dự kiến
                public const string BACK_LOG = "BACK_LOG";

                // đang thực hiện
                public const string IN_PROGRESS = "IN_PROGRESS";

                // hoàn thành
                public const string DONE = "DONE";
            }
        }
    }
}