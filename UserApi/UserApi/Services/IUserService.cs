using UserApi.Models;

namespace UserApi.Services
{
    public interface IUserService
    {
        User? Insert(User el);
        User? Update(User el);
        Task<User?> ValidateAccount(string email, string urlValidation);
        Task<User> GetUser(string email);
    }
}
