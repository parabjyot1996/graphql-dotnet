using GraphQL;
using GraphQL.Types;
using GraphQLServer.Contracts;
using GraphQLServer.Entities;
using GraphQLServer.GraphQL.Types;

namespace GraphQLServer.GraphQL.Mutations
{
    public class AppMutation : ObjectGraphType
    {
        public AppMutation(IOwnerRepository ownerRepository,
                            IAccountRepository accountRepository)
        {
            #region Owner Mutation
            Field<OwnerType>(
                "createOwner",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<OwnerInputType>> { Name = "owner"}),
                resolve: context => 
                {
                    var owner = context.GetArgument<Owner>("owner");
                    return ownerRepository.CreateOwner(owner).Result;
                }
            );

            Field<OwnerType>(
                "updateOwner",
                arguments: new QueryArguments(
                            new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "ownerId" },
                            new QueryArgument<NonNullGraphType<OwnerInputType>> { Name = "owner" }),
                resolve: context => {
                    var ownerId = context.GetArgument<int>("ownerId");
                    var owner = context.GetArgument<Owner>("owner");

                    var updatedOwner = ownerRepository.UpdateOwner(ownerId, owner).Result;
                    if (updatedOwner == null)
                    {
                        context.Errors.Add(new ExecutionError($"Cannot find owner with ID { ownerId }"));
                        return null;
                    }

                    return updatedOwner;
                }
            );

            Field<StringGraphType>(
                "deleteOwner",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "ownerId" }
                ),
                resolve: context => {
                    var ownerId = context.GetArgument<int>("ownerId");

                    if (ownerRepository.DeleteOwner(ownerId).Result)
                    {
                        return $"The owner with Id { ownerId } deleted successfully";
                    }

                    context.Errors.Add(new ExecutionError($"Cannot find owner with ID { ownerId }"));
                    return null;
                }
            );
            #endregion

            #region Account Mutation
            Field<AccountType>(
                "createAccount",
                arguments: new QueryArguments(
                            new QueryArgument<NonNullGraphType<AccountInputType>> { Name = "account" }
                ),
                resolve: context =>  
                {
                    var account = context.GetArgument<Account>("account");
                    if (account.OwnerId == 0)
                    {
                        context.Errors.Add(new ExecutionError($"Please provide ownerId"));
                        return null;
                    }

                    return accountRepository.CreateAccount(account);
                }
            );

            Field<AccountType>(
                "updateAccount",
                arguments: new QueryArguments(
                            new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "accountId" },
                            new QueryArgument<NonNullGraphType<AccountInputType>> { Name = "account" }
                ),
                resolve: context =>
                {
                    var accountId = context.GetArgument<int>("accountId");
                    var account = context.GetArgument<Account>("account");

                    var updatedAccount = accountRepository.UpdateAccount(accountId, account).Result;
                    if (updatedAccount == null)
                    {
                        context.Errors.Add(new ExecutionError($"Cannot find owner with ID { accountId }"));
                        return null;
                    }

                    return updatedAccount;
                }
            );

            Field<StringGraphType>(
                "deleteAccount",
                arguments: new QueryArguments(
                            new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "accountId" }
                ),
                resolve: context => 
                {
                    var accountId = context.GetArgument<int>("accountId");

                    if (accountRepository.DeleteAccount(accountId).Result)
                    {
                        return $"Account with id { accountId } deleted successfully";    
                    }

                    context.Errors.Add(new ExecutionError($"Account with id { accountId } not found"));
                    return null;
                }
            );
            #endregion
        }
    }
}