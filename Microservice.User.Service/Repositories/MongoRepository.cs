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

        public IMongoCollection<UserModel> Users { get; set; }

        public MongoRepository(IOptions<UserDbContext> settings)
        {
            try
            {
                var client = new MongoClient(settings.Value.ConnectionString);
                _mongoDatabase = client.GetDatabase(settings.Value.DatabaseName);
                Users = _mongoDatabase.GetCollection<UserModel>(settings.Value.CollectionName);
            }
            catch (Exception e)
            {
                throw new Exception("Can not access to MongoDb Server.", e);
            }
        }
    }
}
