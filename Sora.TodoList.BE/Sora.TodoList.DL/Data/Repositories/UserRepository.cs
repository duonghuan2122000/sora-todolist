using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
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
    }
}