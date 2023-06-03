using Lec11.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lec11.Helpers
{
    public class UserClaimsFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public UserClaimsFactory(UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
            
            
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim ("UserFirstName", user.FirstName ?? ""));
            identity.AddClaim(new Claim ("UserLastName", user.LastName ?? ""));
            return identity;



        }
    }
}
