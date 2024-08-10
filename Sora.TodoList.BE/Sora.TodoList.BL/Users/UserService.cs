using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Sora.TodoList.BL.Users.Dtos;
using Sora.TodoList.DL.Commons;
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

        /// <summary>
        /// Đăng ký
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        Task<RegisterResDto> Register(RegisterReqDto payload);
    }

    public class UserService : IUserService
    {
        #region Khởi tạo

        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly JwtOption _jwtOption;
        private readonly ITenantRepository _tenantRepository;
        private readonly ITokenRepository _tokenRepository;

        public UserService(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetService<ILogger<UserService>>() ?? NullLogger<UserService>.Instance;
            _userRepository = serviceProvider.GetRequiredService<IUserRepository>();
            _jwtOption = serviceProvider.GetRequiredService<IOptions<JwtOption>>().Value;
            _tenantRepository = serviceProvider.GetRequiredService<ITenantRepository>();
            _tokenRepository = serviceProvider.GetRequiredService<ITokenRepository>();
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

            var (expiresIn, accessToken, refreshToken) = await GrantToken(user);

            return new LoginResDto
            {
                AccessToken = accessToken,
                ExpiresIn = expiresIn,
                RefreshToken = refreshToken
            };
        }

        private async Task<(int, string, string)> GrantToken(UserEntity user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Sid, user.Id),
                new(ClaimTypes.Email, user.Email)
            };

            if (!string.IsNullOrEmpty(user.FirstName))
            {
                claims.Add(new Claim(ClaimTypes.Name, user.FirstName));
            }

            if (!string.IsNullOrEmpty(user.LastName))
            {
                claims.Add(new Claim(ClaimTypes.Surname, user.LastName));
            }

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

            return (expiresIn, new JwtSecurityTokenHandler().WriteToken(token), await GrantRefreshToken(user));
        }

        private async Task<string> GrantRefreshToken(UserEntity user)
        {
            var token = new TokenEntity
            {
                Id = Guid.NewGuid().ToString(),
                TenantId = user.TenantId,
                UserId = user.Id,
                Token = SecureCommon.CreateMD5($"{DateTime.Now.Ticks}|{Guid.NewGuid()}"),
                ExpiredDate = DateTime.Now.AddSeconds(_jwtOption.RefreshTokenExpiresIn),
            };

            await _tokenRepository.Insert(token);

            return token.Token;
        }

        #endregion Hàm login

        #region Hàm đăng ký

        /// <summary>
        /// Đăng ký
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public async Task<RegisterResDto> Register(RegisterReqDto payload)
        {
            var emailExists = await _userRepository.CheckEmailExist(payload.Email);
            if (emailExists)
            {
                throw new TodoListExceptionBase(CommonConst.ErrorInfo.Register.Code.EmailExist, CommonConst.ErrorInfo.Register.Message.EmailExist);
            }

            var tenant = new TenantEntity
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Default",
                ExtraProperties = JsonConvert.SerializeObject(new Dictionary<string, object>()),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            await _tenantRepository.Insert(tenant);

            var user = new UserEntity
            {
                Id = Guid.NewGuid().ToString(),
                TenantId = tenant.Id,
                Email = payload.Email,
                PasswordSalt = _userRepository.GeneratePasswordSalt(),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            user.PasswordHash = _userRepository.HashPassword(payload.Password, user.PasswordSalt);
            await _userRepository.Insert(user);

            var (expiresIn, accessToken, refreshToken) = await GrantToken(user);

            return new RegisterResDto
            {
                AccessToken = accessToken,
                ExpiresIn = expiresIn,
                RefreshToken = refreshToken
            };
        }

        #endregion Hàm đăng ký
    }
}