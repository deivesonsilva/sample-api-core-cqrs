using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SampleApiCoreCqrs.Application.Common.Interfaces;

namespace SampleApiCoreCqrs.Application.Common.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            string accountId = httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Hash)?.Value;
            string firstName = httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name)?.Value;
            string roles = httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Role)?.Value;

            //AccountId = string.IsNullOrEmpty(accountId) ? Guid.Empty : new Guid(accountId);
            //FirstName = firstName ?? string.Empty;
            //Roles = roles ?? string.Empty;

            //used to test
            AccountId = new Guid("e160a647-1807-11eb-9ec3-0242ac130002");
            FirstName = "Antonio Morais";
        }

        public Guid AccountId { get; }
        public string FirstName { get; }
        public string Roles { get; }
    }
}
