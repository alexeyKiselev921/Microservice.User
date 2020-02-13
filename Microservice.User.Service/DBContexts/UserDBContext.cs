namespace Microservice.User.Service.DBContexts
{
    public class UserDbContext : IDbContext
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}