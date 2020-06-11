using GraphQL.Types;

namespace GraphQLServer.GraphQL.Types
{
    public class OwnerInputType : InputObjectGraphType
    {
        public OwnerInputType()
        {
            Name = "OwnerInput";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<StringGraphType>>("address");
            Field<ListGraphType<AccountInputType>>("accounts");
        }
    }
}