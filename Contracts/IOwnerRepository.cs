using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL_POC.Entities;

namespace GraphQL_POC.Contracts
{
    public interface IOwnerRepository
    {
        IObservable<Owner> WhenOwnerCreated();

        IEnumerable<Owner> GetAll();

        Owner GetOwnerById(int id);

        Task<Owner> CreateOwner(Owner owner);

        Task<Owner> UpdateOwner(int ownerId, Owner owner);

        Task<bool> DeleteOwner(int ownerId);
    }
}