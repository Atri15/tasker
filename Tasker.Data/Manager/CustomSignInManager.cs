using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Tasker.Data.Model;
using System;
using System.Data.Entity.Utilities;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;

namespace Tasker.Data.Manager
{
    public class CustomSignInManager : SignInManager<User, Guid>
    {
        public CustomSignInManager(CustomUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            if (UserManager == null)
            {
                return SignInStatus.Failure;
            }

            var user = await UserManager.FindByNameAsync(userName).WithCurrentCulture();

            if (user != null)
            {
                await base.SignInAsync(user, isPersistent, shouldLockout);

                return SignInStatus.Success;
            }

            return SignInStatus.Failure;
        }

        public static CustomSignInManager Create(IdentityFactoryOptions<CustomSignInManager> options, IOwinContext context)
        {
            return new CustomSignInManager(context.GetUserManager<CustomUserManager>(), context.Authentication);
        }
    }
}