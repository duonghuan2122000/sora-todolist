using Dapper;
using Sora.TodoList.DL.Data.Entities;
using System;
using System.Threading.Tasks;

namespace Sora.TodoList.DL.Data.Repositories
{
    public interface ITokenRepository
    {
        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Insert(TokenEntity entity);
    }

    public class TokenRepository : TodoListRepositoryBase, ITokenRepository
    {
        #region Khởi tạo

        public TokenRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        #endregion Khởi tạo

        #region Hàm thêm mới

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Insert(TokenEntity entity)
        {
            using var conn = _dbContext.GetConnection();
            await conn.ExecuteAsync(
                "INSERT INTO sora_token (Id, TenantId, UserId, TokenType, Token, ExpiredDate, CreatedDate, UpdatedDate) VALUES(@Id, @TenantId, @UserId, @TokenType, @Token, @ExpiredDate, @CreatedDate, @UpdatedDate);",
                entity);
        }

        #endregion Hàm thêm mới
    }
}