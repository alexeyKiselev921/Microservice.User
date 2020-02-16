using Newtonsoft.Json.Serialization;

namespace Microservice.User.Service.Resolvers
{
    public class LowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }
}