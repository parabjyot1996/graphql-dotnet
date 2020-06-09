using GraphQL.Types;

namespace GraphQL_POC.GraphQL.Types
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