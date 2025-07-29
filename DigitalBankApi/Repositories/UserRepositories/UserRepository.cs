using DigitalBankApi.Context;
using DigitalBankApi.Models;
using Microsoft.EntityFrameworkCore;
using DigitalBankApi.Repositories.BanckAccountRepositories;
using DigitalBankApi.ResultModels;
using DigitalBankApi.Services;
namespace DigitalBankApi.Repositories.UserRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IBanckAccountRepository _banckAccountRepository;

        public UserRepository(ApplicationDbContext context, IBanckAccountRepository banckAccountRepository)
        {
            _context = context;
            _banckAccountRepository = banckAccountRepository;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
        }

        public async Task<CreateUserResult> CreateUser(User user)
        {
            User userDb = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);

            if (userDb != null)
                return new CreateUserResult { Success = false, Message = "User already exists" };

            _context.Users.Add(user);

            int rowsAffected = await _context.SaveChangesAsync();

            if (rowsAffected == 0) return new CreateUserResult { Success = false, Message = "User not saved" };

            return new CreateUserResult { Success = true, Message = "User created", User = user }; ;
        }

        public async Task<bool> UpdateUser(string email, User user)
        {
            User existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (existingUser == null) return false;

            existingUser.Email = user.Email;
            existingUser.IsEnabled = user.IsEnabled;

            int rowsAffected = await _context.SaveChangesAsync();

            if (rowsAffected == 0) return false;

            return true;
        }

        public async Task<bool> DeleteUser(string email)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
                return false;

            _context.Users.Remove(user);

            int rowsAffected = await _context.SaveChangesAsync();

            if (rowsAffected == 0) return false;

            return true;
        }
    }
}
