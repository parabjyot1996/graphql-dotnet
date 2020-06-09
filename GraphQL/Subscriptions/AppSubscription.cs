using GraphQL.Resolvers;
using GraphQL.Types;
using GraphQL_POC.Contracts;
using GraphQL_POC.Entities;
using GraphQL_POC.GraphQL.Types;

namespace GraphQL_POC.GraphQL.Subscriptions
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