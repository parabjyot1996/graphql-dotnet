using GraphQL;
using GraphQL.Types;
using GraphQLServer.GraphQL.Mutations;
using GraphQLServer.GraphQL.Queries;
using GraphQLServer.GraphQL.Subscriptions;

namespace GraphQLServer.GraphQL.Schemas
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