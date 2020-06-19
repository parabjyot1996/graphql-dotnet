using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLServer.Contracts;
using GraphQLServer.Entities;
using GraphQLServer.Entities.Context;
using Microsoft.EntityFrameworkCore;

namespace GraphQLServer.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationContext _context;

        public AccountRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Account> GetAccountByOwnerId(int ownerId)
        {
            return _context.Accounts.Where(acc => acc.OwnerId == ownerId).ToList();
        }

        public IEnumerable<Account> GetAllAccount()
        {
            return _context.Accounts.ToList();
        }

        public Account GetAccountById(int id)
        {
            return _context.Accounts.SingleOrDefault(acc => acc.Id == id);
        }

        public async Task<Account> CreateAccount(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<Account> UpdateAccount(int accountId, Account account)
        {
            var accountDb = _context.Accounts.SingleOrDefault(acc => acc.Id == accountId);

            if (accountDb == null)
            {
                return null;
            }

            accountDb.Type = account.Type;
            accountDb.Description = account.Description;
            accountDb.OwnerId = account.OwnerId;

            _context.Accounts.Update(accountDb);
            await _context.SaveChangesAsync();

            return accountDb;
        }

        public async Task<bool> DeleteAccount(int accountId)
        {
            var account = _context.Accounts.SingleOrDefault(acc => acc.Id == accountId);

            if (account == null)
            {
                return false;
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ILookup<int, Account>> GetAccountsByOwnerIds(IEnumerable<int> ownerIds)
        {
            var accounts = await _context.Accounts.Where(a => ownerIds.Contains(a.OwnerId)).ToListAsync();
            return accounts.ToLookup(x => x.OwnerId);
        }
    }
}