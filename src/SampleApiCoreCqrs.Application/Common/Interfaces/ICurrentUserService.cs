using System;
namespace SampleApiCoreCqrs.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        Guid AccountId { get; }
        string FirstName { get; }
        string Roles { get; }
    }
}
