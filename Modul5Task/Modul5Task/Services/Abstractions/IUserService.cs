using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modul5Task.Models;
using Modul5Task.Responses;

namespace Modul5Task.Services.Abstractions
{
    public interface IUserService
    {
        Task<User> GetUserById(int id);
        Task<UserResponse> CreateUser(string name, string job);
        Task<UpdateUserResponse> UpdateUser(string name, string job, int id);
        Task DeleteUser(int id);
        Task<List<User>> GetUsers(int page, int per_page = 6, int delay = 0);
        Task RegisterUser(string email, string password);
        Task LoginUser(string email, string password);
    }
}
