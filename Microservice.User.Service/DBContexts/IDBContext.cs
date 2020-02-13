namespace Microservice.User.Service.DBContexts
{
    public interface IDbContext
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}