using Dapper;
using Sora.TodoList.DL.Data.Etos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Sora.TodoList.DL.Data.Repositories
{
    public interface ITaskItemRepository
    {
        /// <summary>
        /// Lấy danh sách công việc
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="payload"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetList<TEntity>(GetListTaskItemEto payload);

        /// <summary>
        /// Tính tổng công việc
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        Task<int> Count(GetListTaskItemEto payload);

        /// <summary>
        /// Lấy thông tin công việc
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> Get<TEntity>(string id);
    }

    public class TaskItemRepository : TodoListRepositoryBase, ITaskItemRepository
    {
        #region Khởi tạo

        public TaskItemRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        #endregion Khởi tạo

        #region Hàm lấy danh sách công việc

        /// <summary>
        /// Lấy danh sách công việc
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="payload"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetList<TEntity>(GetListTaskItemEto payload)
        {
            using var conn = _dbContext.GetConnection();

            var sql = "select * from sora_taskitem st where st.TenantId = @TenantId and st.UpdatedDate >= @FromDate and st.UpdatedDate <= @ToDate";

            var param = new DynamicParameters();
            param.Add("TenantId", _contextService.TenantId);
            param.Add("FromDate", payload.FromDate);
            param.Add("ToDate", payload.ToDate);

            if (payload.StatusList.Count > 0)
            {
                sql += " and st.Status in @StatusList";
                param.Add("StatusList", payload.StatusList);
            }

            sql += " limit @Skip, @Limit";
            param.Add("Skip", payload.Skip);
            param.Add("Limit", payload.Limit);

            var entities = await conn.QueryAsync<TEntity>(sql, param);
            return [.. entities];
        }

        /// <summary>
        /// Tính tổng công việc
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public async Task<int> Count(GetListTaskItemEto payload)
        {
            using var conn = _dbContext.GetConnection();

            var sql = "select count(*) from sora_taskitem st where st.TenantId = @TenantId and st.UpdatedDate >= @FromDate and st.UpdatedDate <= @ToDate";

            var param = new DynamicParameters();
            param.Add("TenantId", _contextService.TenantId);
            param.Add("FromDate", payload.FromDate);
            param.Add("ToDate", payload.ToDate);

            if (payload.StatusList.Count > 0)
            {
                sql += " and st.Status in @StatusList";
                param.Add("StatusList", payload.StatusList);
            }

            var totalCount = await conn.QueryFirstOrDefaultAsync<int?>(sql, param);

            return totalCount ?? 0;
        }

        #endregion Hàm lấy danh sách công việc

        #region Hàm lấy thông tin công việc

        /// <summary>
        /// Lấy thông tin công việc
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity> Get<TEntity>(string id)
        {
            using var conn = _dbContext.GetConnection();

            var sql = "select * from sora_taskitem st where st.TenantId = @TenantId and st.Id = @Id";
            var param = new DynamicParameters();
            param.Add("TenantId", _contextService.TenantId);
            param.Add("Id", id);

            var entity = await conn.QueryFirstOrDefaultAsync<TEntity>(sql, param);
            return entity;
        }

        #endregion Hàm lấy thông tin công việc
    }
}