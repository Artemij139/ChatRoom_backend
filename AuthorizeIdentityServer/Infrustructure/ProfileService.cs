using AuthorizeIdentityServer.Models;
using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AuthorizeIdentityServer.Infrustructure
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {

            var id = context.Subject.GetSubjectId();

            var user = _userManager.FindByIdAsync(id).GetAwaiter().GetResult();

            var claims = new List<Claim>
            {
                new Claim ("name", user.UserName),
               
            };
            context.IssuedClaims.AddRange(claims);

            return Task.CompletedTask;

        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.CompletedTask;
        }
    }
}
