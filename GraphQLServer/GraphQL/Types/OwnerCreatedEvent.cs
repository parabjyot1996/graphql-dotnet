using GraphQL.DataLoader;
using GraphQLServer.Contracts;

namespace GraphQLServer.GraphQL.Types
{
    public class OwnerCreatedEvents : OwnerType
    {
        public OwnerCreatedEvents(IAccountRepository repository, IDataLoaderContextAccessor dataLoader) 
            : base(repository, dataLoader)
        {
        }
    }
}