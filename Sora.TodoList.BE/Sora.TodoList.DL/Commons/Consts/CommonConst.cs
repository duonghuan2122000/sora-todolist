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
        }
    }
}