using GraphQL.Resolvers;
using GraphQL.Types;
using GraphQLServer.Contracts;
using GraphQLServer.Entities;
using GraphQLServer.GraphQL.Types;

namespace GraphQLServer.GraphQL.Subscriptions
{
    public class AppSubscription : ObjectGraphType<object>
    {
        public AppSubscription(IOwnerRepository repository)
        {
            Name = "Subscription";
            AddField(
                new EventStreamFieldType()
                {
                    Name = "ownerAdded",
                    Description = "Subscribe to owner created event",
                    Type = typeof(OwnerCreatedEvents),
                    Resolver = new FuncFieldResolver<Owner>(context => context.Source as Owner),
                    Subscriber = new EventStreamResolver<Owner>(context => repository.WhenOwnerCreated())
                }
            );
        }
    }
}