using GraphQL.Types;

namespace GraphQLServer.GraphQL.Types
{
    public class AccountInputType : InputObjectGraphType
    {
        public AccountInputType()
        {
            Name = "AccountInput";
            Field<StringGraphType>("description");
            Field<AccountTypeEnumType>("type");
            Field<IdGraphType>("ownerId");
        }
    }
}