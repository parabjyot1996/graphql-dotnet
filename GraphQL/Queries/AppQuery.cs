using GraphQL.Types;
using GraphQL_POC.Contracts;
using GraphQL_POC.GraphQL.Types;

namespace GraphQL_POC.GraphQL.Queries
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(IOwnerRepository ownerRepository, 
                        IAccountRepository accountRepository)
        {
            #region Owner
            Field<ListGraphType<OwnerType>>(
                "owners",
                resolve: context => ownerRepository.GetAll()
            );

            Field<OwnerType>(
                "owner",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id"}),
                resolve: context => ownerRepository.GetOwnerById(context.GetArgument<int>("id"))
            );
            #endregion

            #region Account
            Field<ListGraphType<AccountType>>(
                "accounts",
                resolve: context => accountRepository.GetAllAccount()
            );

            Field<AccountType>(
                "account",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context => accountRepository.GetAccountById(context.GetArgument<int>("id"))
            );
            #endregion
        }
    }
}