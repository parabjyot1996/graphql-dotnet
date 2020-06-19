using System;
using System.Linq;
using GraphQL.DataLoader;
using GraphQL.Types;
using GraphQLServer.Contracts;
using GraphQLServer.Entities;

namespace GraphQLServer.GraphQL.Types
{
    public class OwnerType : ObjectGraphType<Owner>
    {
        public OwnerType(IAccountRepository repository,
                            IDataLoaderContextAccessor dataLoader)
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id property from owner object");
            Field(x => x.Name).Description("Name property from the owner object");
            Field(x => x.Address).Description("Address property from the owner object.");
            Field<ListGraphType<AccountType>>(
                "accounts",
                //resolve: context => repository.GetAccountByOwnerId(context.Source.Id)
                resolve: context => 
                {
                    var loader = dataLoader.Context
                                    .GetOrAddCollectionBatchLoader<int, Account>("GetAccountsByOwnerIds", repository.GetAccountsByOwnerIds);
                    return loader.LoadAsync(context.Source.Id);
                }
            );
        }
    }
}