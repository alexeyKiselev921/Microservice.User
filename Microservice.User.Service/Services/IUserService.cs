using System.Collections.Generic;
using System.Threading.Tasks;
using Microservice.User.Service.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Microservice.User.Service.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAll();
        Task<UserModel> Get(string userId);
        Task Create(UserModel user);
        Task<bool> Update(UserModel user);
        Task<DeleteResult> Remove(string userId);
        Task<DeleteResult> RemoveAll();
        Task<UserModel> Login(string username, string password);
        Task<UserModel> GetUser(string username);
    }
}