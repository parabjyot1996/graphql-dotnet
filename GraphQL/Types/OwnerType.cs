using System.Linq;
using GraphQL.Types;
using GraphQL_POC.Contracts;
using GraphQL_POC.Entities;

namespace GraphQL_POC.GraphQL.Types
{
    public class OwnerType : ObjectGraphType<Owner>
    {
        public OwnerType(IAccountRepository repository)
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id property from owner object");
            Field(x => x.Name).Description("Name property from the owner object");
            Field(x => x.Address).Description("Address property from the owner object.");
            Field<ListGraphType<AccountType>>(
                "accounts",
                resolve: context => repository.GetAccountByOwnerId(context.Source.Id)
            );
        }
    }
}