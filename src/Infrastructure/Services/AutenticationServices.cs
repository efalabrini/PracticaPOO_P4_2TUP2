using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class CustomAuthenticationService : ICustomAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly AutenticacionServiceOptions _options;

        // Constructor
        public CustomAuthenticationService(IUserRepository userRepository, IOptions<AutenticacionServiceOptions> options)
        {
            _userRepository = userRepository;
            _options = options.Value;
        }

        // Método privado para validar usuario
        private User? ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _userRepository.GetUserByUserName(username);
            if (user == null) return null;

            if (user.Password == password) return user;

            return null;
        }

        // Método público que implementa la interface
        public string Autenticar(string username, string password)
        {
            var user = ValidateUser(username, password);
            if (user == null)
                throw new NotFoundException("User authentication failed");

            // Crear token JWT
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("given_name", user.UserName),
                new Claim("family_name", user.LastName),
            };

            var jwtToken = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        // Opciones de configuración
        public class AutenticacionServiceOptions
        {
            public const string SectionName = "AutenticacionService";
            public string Issuer { get; set; } = string.Empty;
            public string Audience { get; set; } = string.Empty;
            public string SecretForKey { get; set; } = string.Empty;
        }
    }
}
