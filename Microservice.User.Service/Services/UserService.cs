using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microservice.User.Service.DBContexts;
using Microservice.User.Service.Models;
using Microservice.User.Service.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Microservice.User.Service.Services
{
    public class UserService : IUserService
    {
        private readonly MongoRepository _repository = null;

        public UserService(IOptions<UserDbContext> settings)
        {
            _repository = new MongoRepository(settings);
        }

        public async Task<IEnumerable<UserModel>> GetAll()
        {
            return await _repository.Users.Find(x => true).ToListAsync();
        }

        public async Task<UserModel> Get(string userId)
        {
            var filter = Builders<UserModel>.Filter.Eq("Id", userId);
            return await _repository.Users.Find(filter).FirstOrDefaultAsync();
        }

        public async Task Create(UserModel user)
        {
            await _repository.Users.InsertOneAsync(user);
        }

        public async Task<bool> Update(UserModel user)
        {
            var filter = Builders<UserModel>.Filter.Eq("Id", user.Id);
            var updatingUser = _repository.Users.Find(filter).FirstOrDefaultAsync();
            if (updatingUser.Result == null)
                return false;
            var update = Builders<UserModel>.Update
                .Set(x => x.Username, user.Username)
                .Set(x => x.Password, user.Password)
                .Set(x => x.Email, user.Email);
            await _repository.Users.UpdateOneAsync(filter, update);
            return true;

        }

        public async Task<DeleteResult> Remove(string userId)
        {
            var filter = Builders<UserModel>.Filter.Eq("Id", userId);
            return await _repository.Users.DeleteOneAsync(filter);
        }

        public async Task<DeleteResult> RemoveAll()
        {
            return await _repository.Users.DeleteManyAsync(new BsonDocument());
        }

        public async Task<UserModel> Login(string username, string password)
        {
            var userNameFilter = Builders<UserModel>.Filter.Eq("Username", username);
            var passwordFilter = Builders<UserModel>.Filter.Eq("Password", password);
            var filter = passwordFilter & userNameFilter;
            return await _repository.Users.Find(filter).FirstOrDefaultAsync();
        }
    }
}