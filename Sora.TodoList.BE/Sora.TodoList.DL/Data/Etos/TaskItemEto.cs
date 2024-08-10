using System;
using System.Collections.Generic;

namespace Sora.TodoList.DL.Data.Etos
{
    /// <summary>
    /// Dto req lấy danh sách công việc
    /// </summary>
    public class GetListTaskItemEto
    {
        /// <summary>
        /// Vị trí lấy
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// Số lượng bản ghi
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Thời gian bắt đầu
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Thời gian kết thúc
        /// </summary>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Id user được assign
        /// </summary>
        public string UserIdAssign { get; set; }

        public List<string> StatusList { get; set; } = new List<string>();
    }
}