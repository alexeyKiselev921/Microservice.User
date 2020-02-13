using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.User.Service.DBContexts;
using Microservice.User.Service.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Microservice.User.Service.Repositories
{
    public class MongoRepository
    {
        private readonly IMongoDatabase _mongoDatabase;

        public IMongoCollection<UserModel> Users => _mongoDatabase.GetCollection<UserModel>("Users");

        public MongoRepository(IOptions<UserDbContext> settings)
        {
            try
            {
                var client = new MongoClient(settings.Value.ConnectionString);
                _mongoDatabase = client.GetDatabase(settings.Value.DatabaseName);
            }
            catch (Exception e)
            {
                throw new Exception("Can not access to MongoDb Server.", e);
            }
        }
    }
}
