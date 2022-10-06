using Identity.Domain;
using IdentityModel;
using System.Security.Claims;

namespace Identity.Services
{
    public static class ExtentionService
    {
        public static Claim[] ToClaims(this User user)
        {
            //define the list of claim
            return new[]
            {
                new Claim(JwtClaimTypes.PreferredUserName,user.UserName),
                new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
                new Claim(JwtClaimTypes.Name, user.UserName),
                new Claim(JwtClaimTypes.Email, user.Email),
                new Claim(JwtClaimTypes.Role, user.Role),
                new Claim("TenantId", user.TenantId.ToString())
            };
        }
    }
}
