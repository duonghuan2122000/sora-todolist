namespace Sora.TodoList.BL.Users.Dtos
{
    public class JwtOption
    {
        public const string KeyConfig = "Jwt";

        /// <summary>
        /// Key jwt
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Issuer
        /// </summary>
        public string Issuer { get; set; }

        public int ExpiresIn { get; set; } = 86400;

        public int RefreshTokenExpiresIn { get; set; } = 2592000;
    }
}