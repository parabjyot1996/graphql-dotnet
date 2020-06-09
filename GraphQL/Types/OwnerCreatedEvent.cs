using GraphQL_POC.Contracts;

namespace GraphQL_POC.GraphQL.Types
{
    public class OwnerCreatedEvents : OwnerType
    {
        public OwnerCreatedEvents(IAccountRepository repository) : base(repository)
        {
        }
    }
}