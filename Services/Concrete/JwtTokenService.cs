using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UniBook.Services.Abstract;

namespace UniBook.Services.Concrete
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string name, string surname, string userName, List<string> roles)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));

            var claims = new List<Claim>()
            {
                new Claim("UserName", userName),
                new Claim("Name", name),
                new Claim("Surname", surname)
            };

            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));

            var token = new JwtSecurityToken(expires: DateTime.Now.AddMinutes(10),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256), claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
