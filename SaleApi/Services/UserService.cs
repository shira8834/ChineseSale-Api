using SaleApi.Models;
using SaleApi.Repositories;

namespace SaleApi.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //כל המשתמשים
        public async Task<IEnumerable<User>> GetAllUser()
        {
            var user = await _userRepository.GetAllUser();
            return user ?? Enumerable.Empty<User>();
        }

    }
}
