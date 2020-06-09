using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL_POC.Entities;

namespace GraphQL_POC.Contracts
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAccountByOwnerId(int ownerId);

        IEnumerable<Account> GetAllAccount();

        Account GetAccountById(int id);

        Task<Account> CreateAccount(Account account);

        Task<Account> UpdateAccount(int accountId, Account account);

        Task<bool> DeleteAccount(int accountId);
    }
}