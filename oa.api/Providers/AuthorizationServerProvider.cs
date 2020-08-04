using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using oa.services.Business;
using oa.services.Personnel;
using oa.services.User;
using System.Security.Claims;
using System.Threading.Tasks;

namespace oa.api.Providers
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUserService _userService;
        private readonly IPersonnelService _personnelService;
        public AuthorizationServerProvider(IUserService userService, IPersonnelService personnelService)
        {
            _userService = userService;
            _personnelService = personnelService;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var role = "";
            var emailAddress = "";


            var personnel = await _personnelService.FindByCredentials(context.UserName, context.Password);
            var user = await _userService.FindByCredentials(context.UserName, context.Password);
            if (personnel != null)
            {
                emailAddress = personnel.Email;
                role = personnel.Role;
            }
            else if (user != null)
            {
                emailAddress = user.Email;
                role = "User";
            }
            else
            {
                context.SetError("Invalid grant_type", "The user name or password is incorrect.");
                return;
            }

  
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, emailAddress));
            identity.AddClaim(new Claim(ClaimTypes.Role, role));


            context.Validated(identity);

        }


    }
}