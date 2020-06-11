using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using GraphQLServer.Contracts;
using GraphQLServer.Entities;
using GraphQLServer.Entities.Context;
using Microsoft.EntityFrameworkCore;

namespace GraphQLServer.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly ISubject<Owner> _whenOwnerCreated;

        private readonly ApplicationContext _context;

        //private List<Owner> owners;    

        public OwnerRepository(ApplicationContext context)
        {
            // owners = new List<Owner>
            // {
            //     new Owner { Id = 1, Name = "Parab", Address = "Mumbai",
            //                 Accounts = new List<Account> { 
            //                     new Account { Id = 1, Description = "Savings Account", Type = TypeOfAccount.Savings }
            //                 }},
            //     new Owner { Id = 2, Name = "Test1", Address = "Delhi" },
            //     new Owner { Id = 3, Name = "Test2", Address = "Indore" },
            //     new Owner { Id = 4, Name = "Test3", Address = "Chennai",
            //                 Accounts = new List<Account> { 
            //                     new Account { Id = 2, Description = "Expense Account", Type = TypeOfAccount.Expense }
            //                 }}
            // };
            _context = context;
            _whenOwnerCreated = new ReplaySubject<Owner>(1);
        }

        public IObservable<Owner> WhenOwnerCreated()
        {
            return _whenOwnerCreated.AsObservable();
        }

        public async Task<Owner> CreateOwner(Owner owner)
        {
            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();
            this._whenOwnerCreated.OnNext(owner);
            return owner;
        }

        public async Task<bool> DeleteOwner(int ownerId)
        {
            var dbOwner = _context.Owners.SingleOrDefault(o => o.Id == ownerId);

            if (dbOwner == null)
            {
                return false;
            }

            _context.Owners.Remove(dbOwner);
            await _context.SaveChangesAsync();

            return true;
        }

        public IEnumerable<Owner> GetAll()
        {
            return _context.Owners.ToList();
        }

        public Owner GetOwnerById(int id)
        {
            return _context.Owners.SingleOrDefault(o => o.Id == id);
        }

        public async Task<Owner> UpdateOwner(int ownerId, Owner owner)
        {
            var dbOwner = _context.Owners.Include(o => o.Accounts).SingleOrDefault(o => o.Id == ownerId);

            if (dbOwner == null)
            {
                return null;
            }

            dbOwner.Name = owner.Name;
            dbOwner.Address = owner.Address;

            foreach (var account in owner.Accounts)
            {
                dbOwner.Accounts.Add(account);
            }

            _context.Owners.Update(dbOwner);
            await _context.SaveChangesAsync();
            return dbOwner;
        }
    }
}