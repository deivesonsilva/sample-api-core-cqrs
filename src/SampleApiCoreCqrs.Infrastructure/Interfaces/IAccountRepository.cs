using System;
using SampleApiCoreCqrs.Infrastructure.Entities;

namespace SampleApiCoreCqrs.Infrastructure.Interfaces
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
    }
}
