namespace Domain.Abstractions
{
    public interface IAuthService
    {
        public string GenerateJWT(string email, string username);
    }
}
