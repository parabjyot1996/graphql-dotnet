using GraphQLServer.Contracts;

namespace GraphQLServer.GraphQL.Types
{
    public class OwnerCreatedEvents : OwnerType
    {
        public OwnerCreatedEvents(IAccountRepository repository) : base(repository)
        {
        }
    }
}