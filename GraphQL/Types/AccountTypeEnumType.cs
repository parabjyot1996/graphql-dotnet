using GraphQL.Types;
using GraphQL_POC.Entities;

namespace GraphQL_POC.GraphQL.Types
{
    public class AccountTypeEnumType : EnumerationGraphType<TypeOfAccount>
    {
        public AccountTypeEnumType()
        {
            Name = "Type";
            Description = "Enumeration for the account type object";
        }
    }
}