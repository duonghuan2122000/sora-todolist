namespace Sora.TodoList.HttpApi.Dtos
{
    /// <summary>
    /// Dto res base
    /// </summary>
    public class ResultResDtoBase
    {
        public bool Success { get; set; }

        /// <summary>
        /// Mã lỗi
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Thông báo mã lỗi
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        public object Data { get; set; }
    }
}