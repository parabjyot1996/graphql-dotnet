using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLServer.Entities;

namespace GraphQLServer.Contracts
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAccountByOwnerId(int ownerId);

        Task<ILookup<int, Account>> GetAccountsByOwnerIds(IEnumerable<int> ownerIds);

        IEnumerable<Account> GetAllAccount();

        Account GetAccountById(int id);

        Task<Account> CreateAccount(Account account);

        Task<Account> UpdateAccount(int accountId, Account account);

        Task<bool> DeleteAccount(int accountId);
    }
}