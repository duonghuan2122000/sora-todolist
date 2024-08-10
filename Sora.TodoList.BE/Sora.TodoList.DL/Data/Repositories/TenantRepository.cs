using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Sora.TodoList.DL.Data.Entities;
using System;
using System.Threading.Tasks;

namespace Sora.TodoList.DL.Data.Repositories
{
    public interface ITenantRepository
    {
        /// <summary>
        /// Thêm mới tenant
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Insert(TenantEntity entity);
    }

    public class TenantRepository : TodoListRepositoryBase, ITenantRepository
    {
        #region Khởi tạo

        private readonly ILogger<TenantRepository> _logger;

        public TenantRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = serviceProvider.GetService<ILogger<TenantRepository>>() ?? NullLogger<TenantRepository>.Instance;
        }

        #endregion Khởi tạo

        #region Hàm thêm mới tenant

        /// <summary>
        /// Thêm mới tenant
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Insert(TenantEntity entity)
        {
            using var conn = _dbContext.GetConnection();
            await conn.ExecuteAsync(
                "INSERT INTO sora_tenant (Id, Name, ExtraProperties, CreatedDate, UpdatedDate) VALUES(@Id, @Name, '{}', @CreatedDate, @UpdatedDate);",
                entity);
        }

        #endregion Hàm thêm mới tenant
    }
}