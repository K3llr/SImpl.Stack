using System.Collections.Generic;
using System.Security.Claims;

namespace SImpl.Http.OAuth.Api
{
    public interface IOAuthUserProvider
    {
        string GetUserId(string username);
        bool ValidateUser(string username, string password, List<string> scopes);
        IEnumerable<Claim> GetClaimsByUserId(string userId);
        IEnumerable<Claim> GetClaimsByUsername(string username);
        void RegenerateRememberToken(string userId);
        string GetUserRememberToken(string userId);
    }
}