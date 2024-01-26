namespace UniBook.Services.Abstract
{
    public interface IJwtTokenService
    {
        public string GenerateToken(string name, string surname, string userName, List<string> roles);
    }
}
