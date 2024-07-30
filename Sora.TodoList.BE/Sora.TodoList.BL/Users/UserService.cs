using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sora.TodoList.BL.Users.Dtos;
using Sora.TodoList.DL.Commons.Consts;
using Sora.TodoList.DL.Commons.Exceptions;
using Sora.TodoList.DL.Data.Entities;
using Sora.TodoList.DL.Data.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sora.TodoList.BL.Users
{
    public interface IUserService
    {
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        Task<LoginResDto> Login(LoginReqDto payload);
    }

    public class UserService : IUserService
    {
        #region Khởi tạo

        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly JwtOption _jwtOption;

        public UserService(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetService<ILogger<UserService>>() ?? NullLogger<UserService>.Instance;
            _userRepository = serviceProvider.GetRequiredService<IUserRepository>();
            _jwtOption = serviceProvider.GetRequiredService<IOptions<JwtOption>>().Value;
        }

        #endregion Khởi tạo

        #region Hàm login

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public async Task<LoginResDto> Login(LoginReqDto payload)
        {
            var user = await _userRepository.GetByEmail<UserEntity>(payload.Email) ?? throw new TodoListExceptionBase(CommonConst.ErrorInfo.Login.Code.EmailInvalid, CommonConst.ErrorInfo.Login.Message.EmailInvalid);

            // kiểm tra mật khẩu
            if (!_userRepository.ComparePassword(payload.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new TodoListExceptionBase(CommonConst.ErrorInfo.Login.Code.PasswordInvalid, CommonConst.ErrorInfo.Login.Message.PasswordInvalid);
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Sid, user.Id),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Surname, user.LastName),
                new(ClaimTypes.GivenName, user.FirstName)
            };

            if (!string.IsNullOrEmpty(user.TenantId))
            {
                claims.Add(new Claim("tenantid", user.TenantId));
            }

            var expiresIn = _jwtOption.ExpiresIn;

            var token = new JwtSecurityToken(
                issuer: _jwtOption.Issuer,
              audience: _jwtOption.Issuer,
              claims: claims,
              expires: DateTime.Now.AddSeconds(expiresIn),
              signingCredentials: credentials);

            return new LoginResDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresIn = expiresIn,
            };
        }

        #endregion Hàm login
    }
}