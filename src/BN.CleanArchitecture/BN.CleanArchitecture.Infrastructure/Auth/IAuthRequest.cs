using Microsoft.AspNetCore.Authorization;

namespace BN.CleanArchitecture.Infrastructure.Auth;

public interface IAuthRequest : IAuthorizationRequirement
{
}