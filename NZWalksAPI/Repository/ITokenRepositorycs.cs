using Microsoft.AspNetCore.Identity;

namespace NZWalksAPI.Repository
{
    public interface ITokenRepositorycs
    {
        string CreateJWToken(IdentityUser user ,List<string> roles);
        
    }
}
