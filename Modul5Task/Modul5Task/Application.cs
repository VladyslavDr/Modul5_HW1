using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modul5Task.Services.Abstractions;

namespace Modul5Task
{
    public class Application
    {
        private readonly IUserService _userServise;

        public Application(IUserService userServise)
        {
            _userServise = userServise;
        }

        public async Task Start()
        {
            var user = await _userServise.GetUserById(2);
            var user2 = await _userServise.CreateUser("morpheus", "leader");
            var update = await _userServise.UpdateUser("morpheus", "zion resident", 2);
            await _userServise.DeleteUser(2);
            var users = await _userServise.GetUsers(page: 2);
            await _userServise.RegisterUser("eve.holt@reqres.in", "pistol");
            await _userServise.LoginUser("eve.holt@reqres.in", "cityslicka");
        }
    }
}
