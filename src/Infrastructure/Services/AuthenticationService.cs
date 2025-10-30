using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class AuthenticationService
{
    private readonly ApplicationDbContext _context;
    private readonly AuthenticationOptions _options;

    public AuthenticationService(ApplicationDbContext context, IOptions<AuthenticationOptions> options)
    {
        _context = context;
        _options = options.Value;
    }

    private User? ValidateUser(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            return null;

        return _context.Users.FirstOrDefault(u =>
            u.UserName == username && u.Password == password);
    }

    public string Authenticate(string username, string password)
    {
        var user = ValidateUser(username, password);
        if (user == null)
            throw new UnauthorizedAccessException("Usuario o contraseña incorrectos.");

        // Crear el token JWT
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim("sub", user.Id.ToString()),
            new Claim("given_name", user.FirstName),
            new Claim("family_name", user.LastName),
            new Claim("email", user.Email),
            new Claim("role", "User")
        };

        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddHours(1),
            creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public User? Register(User user)
    {
        var existing = _context.Users.FirstOrDefault(u =>
            u.UserName == user.UserName || u.Email == user.Email);

        if (existing != null)
            return null;

        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public class AuthenticationOptions
    {
        public const string SectionName = "AuthenticationService";
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string SecretForKey { get; set; } = string.Empty;
    }
}
