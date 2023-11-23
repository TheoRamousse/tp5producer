using UserApi.Models;

namespace UserApi.Services
{
    public interface IUserService
    {
        int Insert(User el);
        int Update(User el);
        int ValidateAccount(string email, string urlValidation);
        User GetUser(string email);
    }
}
