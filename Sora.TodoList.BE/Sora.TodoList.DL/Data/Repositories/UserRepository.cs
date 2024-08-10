using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Sora.TodoList.DL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sora.TodoList.DL.Data.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Lấy thông tin bằng email
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<TEntity> GetByEmail<TEntity>(string email);

        /// <summary>
        /// Tạo passwordSalt
        /// </summary>
        /// <returns></returns>
        string GeneratePasswordSalt();

        /// <summary>
        /// Hash mật khẩu
        /// </summary>
        /// <param name="rawPassword"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        string HashPassword(string rawPassword, string salt);

        /// <summary>
        /// Kiểm tra mật khẩu
        /// </summary>
        /// <param name="rawPassword"></param>
        /// <param name="passwordHash"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        bool ComparePassword(string rawPassword, string passwordHash, string salt);

        /// <summary>
        /// Kiểm tra email đã tồn tại?
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> CheckEmailExist(string email);

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Insert(UserEntity entity);

        /// <summary>
        /// Lấy danh sách bằng id
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetList<TEntity>(IEnumerable<string> ids);
    }

    public class UserRepository : TodoListRepositoryBase, IUserRepository
    {
        #region Khởi tạo

        private readonly ILogger<UserRepository> _logger;

        public UserRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = serviceProvider.GetService<ILogger<UserRepository>>() ?? NullLogger<UserRepository>.Instance;
        }

        #endregion Khởi tạo

        #region Hàm lấy thông tin bằng email

        /// <summary>
        /// Lấy thông tin bằng email
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<TEntity> GetByEmail<TEntity>(string email)
        {
            using var conn = _dbContext.GetConnection();
            var entity = await conn.QueryFirstOrDefaultAsync<TEntity>(
                "Select * from sora_user where Email = @Email",
                new
                {
                    Email = email
                });
            return entity;
        }

        #endregion Hàm lấy thông tin bằng email

        #region Hàm Tạo passwordSalt

        /// <summary>
        /// Tạo passwordSalt
        /// </summary>
        /// <returns></returns>
        public string GeneratePasswordSalt()
        {
            return Guid.NewGuid().ToString();
        }

        #endregion Hàm Tạo passwordSalt

        #region Hàm tạo và kiểm tra mật khẩu

        /// <summary>
        /// Hash mật khẩu
        /// </summary>
        /// <param name="rawPassword"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public string HashPassword(string rawPassword, string salt)
        {
            return CreateMD5($"{rawPassword}|{salt}").ToUpper();
        }

        /// <summary>
        /// Kiểm tra mật khẩu
        /// </summary>
        /// <param name="rawPassword"></param>
        /// <param name="passwordHash"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public bool ComparePassword(string rawPassword, string passwordHash, string salt)
        {
            return HashPassword(rawPassword, salt).Equals(passwordHash, StringComparison.OrdinalIgnoreCase);
        }

        private static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes); // .NET 5 +
        }

        #endregion Hàm tạo và kiểm tra mật khẩu

        #region Kiểm tra email đã tồn tại?

        /// <summary>
        /// Kiểm tra email đã tồn tại?
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> CheckEmailExist(string email)
        {
            using var conn = _dbContext.GetConnection();
            var userId = await conn.QueryFirstOrDefaultAsync<string>(
                "select Id from sora_user where Email = @Email",
                new
                {
                    Email = email,
                });
            return !string.IsNullOrEmpty(userId);
        }

        #endregion Kiểm tra email đã tồn tại?

        #region Hàm thêm mới

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Insert(UserEntity entity)
        {
            using var conn = _dbContext.GetConnection();
            await conn.ExecuteAsync(
                "INSERT INTO sora_user (Id, TenantId, Email, PasswordHash, PasswordSalt, FirstName, LastName, CreatedDate, UpdatedDate) VALUES(@Id, @TenantId, @Email, @PasswordHash, @PasswordSalt, @FirstName, @LastName, @CreatedDate, @UpdatedDate);",
                entity);
        }

        #endregion Hàm thêm mới

        #region Hàm lấy danh sách user bằng ids

        /// <summary>
        /// Lấy danh sách bằng id
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetList<TEntity>(IEnumerable<string> ids)
        {
            using var conn = _dbContext.GetConnection();

            var sql = "select * from sora_user su where su.TenantId = @TenantId and su.Id in @UserIds";

            var param = new DynamicParameters();
            param.Add("TenantId", _contextService.FindClaimValue("tenantid") ?? string.Empty);
            param.Add("UserIds", ids);

            var entities = await conn.QueryAsync<TEntity>(sql, param);
            return [.. entities];
        }

        #endregion Hàm lấy danh sách user bằng ids
    }
}