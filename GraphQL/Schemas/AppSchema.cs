using GraphQL;
using GraphQL.Types;
using GraphQL_POC.GraphQL.Mutations;
using GraphQL_POC.GraphQL.Queries;
using GraphQL_POC.GraphQL.Subscriptions;

namespace GraphQL_POC.GraphQL.Schemas
{
    public class AppSchema : Schema
    {
        public AppSchema(IDependencyResolver resolver)
            :base(resolver)
        {
            Query = resolver.Resolve<AppQuery>();
            Mutation = resolver.Resolve<AppMutation>();
            Subscription = resolver.Resolve<AppSubscription>();
        }
    }   
}