using System.Security.Cryptography;
using UniBook.Services.Abstract;

namespace UniBook.Services.Concrete
{
    public class PasswordHashService : IPasswordHashService
    {
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash =  hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
