using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Modul5Task.Models;
using Modul5Task.Services.Abstractions;
using Modul5Task.Responses;
using Modul5Task.Requests;
using Modul5Task.Configurations;
using Microsoft.Extensions.Options;

namespace Modul5Task.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly ILogger<UserService> _logger;
        private readonly string _userApi = "api/users/";
        private readonly string _registerApi = "api/register";
        private readonly string _loginApi = "api/login";
        private readonly ApiOption _option;

        public UserService(IHttpClientService httpClientService, ILogger<UserService> logger, IOptions<ApiOption> apiOption)
        {
            _httpClientService = httpClientService;
            _logger = logger;
            _option = apiOption.Value;
        }

        public async Task<User> GetUserById(int id)
        {
            var response = await _httpClientService.SendAsync<BaseResponse<User>, object>($"{_option.Host}{_userApi}{id}", HttpMethod.Get);

            if (response?.Data != null)
            {
                _logger.LogInformation($"User width id = {response.Data.Id} was found!");
            }

            return response?.Data;
        }

        public async Task<UserResponse> CreateUser(string name, string job)
        {
            var response = await _httpClientService.SendAsync<UserResponse, UserRequest>(
                $"{_option.Host}{_userApi}",
                HttpMethod.Post,
                new UserRequest()
                {
                    Job = job,
                    Name = name
                });
            if (response != null)
            {
                _logger.LogInformation($"User with id = {response?.Id} was created");
            }

            return response;
        }

        public async Task<UpdateUserResponse> UpdateUser(string name, string job, int id)
        {
            var response = await _httpClientService.SendAsync<UpdateUserResponse, UserRequest>(
                $"{_option.Host}{_userApi}{id}",
                HttpMethod.Put,
                new UserRequest()
                {
                    Job = job,
                    Name = name
                });
            if (response != null)
            {
                _logger.LogInformation($"User with id = {id} was updated");
            }

            return response;
        }

        public async Task DeleteUser(int id)
        {
            var response = await _httpClientService.SendAsync<object, object>(
                $"{_option.Host}{_userApi}{id}",
                HttpMethod.Delete);
            if (response != null)
            {
                _logger.LogInformation($"User with id = {id} was deleted");
            }
        }

        public async Task<List<User>> GetUsers(int page, int per_page = 6, int delay = 0)
        {
            var url = $"{_option.Host}{_userApi}?page={page}&per_page={per_page}";

            if (delay > 0)
            {
                url += $"&delay={delay}";
            }

            var response = await _httpClientService.SendAsync<BaseResponse<List<User>>, object>(
                url,
                HttpMethod.Get);

            return response?.Data;
        }

        public async Task RegisterUser(string email, string password)
        {
            var response = await _httpClientService.SendAsync<RegisterUserResponse, LoginRequest>(
                $"{_option.Host}{_registerApi}",
                HttpMethod.Post,
                new LoginRequest()
                {
                    Email = email,
                    Password = password
                });
            if (response != null)
            {
                _logger.LogInformation($"Registered user: {email}");
            }
        }

        public async Task LoginUser(string email, string password)
        {
            var response = await _httpClientService.SendAsync<LoginResponse, LoginRequest>(
                $"{_option.Host}{_loginApi}",
                HttpMethod.Post,
                new LoginRequest()
                {
                    Email = email,
                    Password = password
                });
            if (response != null)
            {
                _logger.LogInformation($"Login user: {email}");
            }
        }
    }
}
