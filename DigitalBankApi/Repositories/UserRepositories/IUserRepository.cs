using DigitalBankApi.Models;
using DigitalBankApi.ResultModels;

namespace DigitalBankApi.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        Task<User> GetUserById(string email);
        Task<CreateUserResult> CreateUser(User user);
        Task<bool> UpdateUser(string email, User user);
        Task<bool> DeleteUser(string email);
    }
}
