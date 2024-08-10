using Dapper;
using Newtonsoft.Json;
using Sora.TodoList.DL.Data.Etos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sora.TodoList.DL.Data.Repositories
{
    public interface IConstRepository
    {
        /// <summary>
        /// Lấy danh sách const list
        /// </summary>
        /// <param name="constListKey"></param>
        /// <returns></returns>
        Task<List<ConstItemEto>> GetConstList(string constListKey);
    }

    public class ConstRepository : TodoListRepositoryBase, IConstRepository
    {
        #region Khởi tạo

        public ConstRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        #endregion Khởi tạo

        #region Hàm

        /// <summary>
        /// Lấy danh sách const list
        /// </summary>
        /// <param name="constListKey"></param>
        /// <returns></returns>
        public async Task<List<ConstItemEto>> GetConstList(string constListKey)
        {
            using var conn = _dbContext.GetConnection();
            var entities = await conn.QueryAsync<ConstItemEto>(
                "select sc.ConstKey as `Key`, sc.ConstValue as Value, sc.ExtraProperties as ExtraPropertiesStr from sora_const sc where sc.ConstList = @ConstList;",
                new
                {
                    ConstList = constListKey
                });
            return [.. entities.Select(x => {
                if(!string.IsNullOrEmpty(x.ExtraPropertiesStr)){
                    x.ExtraProperties = JsonConvert.DeserializeObject<Dictionary<string, string>>(x.ExtraPropertiesStr);
                }
                return x;
            })];
        }

        #endregion Hàm
    }
}