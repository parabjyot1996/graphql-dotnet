    using GraphQL.Types;
    using GraphQLServer.Entities;

    namespace GraphQLServer.GraphQL.Types
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