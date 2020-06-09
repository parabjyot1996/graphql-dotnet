using GraphQL.Types;

namespace GraphQL_POC.GraphQL.Types
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