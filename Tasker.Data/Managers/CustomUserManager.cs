using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Tasker.Data.DAL;
using Tasker.Data.Model;
using Tasker.Data.Services;

namespace Tasker.Data.Managers
{
    public class CustomUserManager : UserManager<User, Guid>
    {
        public CustomUserManager(IUserStore<User, Guid> store)
            : base(store)
        {
            PasswordHasher = new CustomPasswordHasher();
        }

        public static CustomUserManager Create(IdentityFactoryOptions<CustomUserManager> options, IOwinContext context)
        {
            var manager = new CustomUserManager(new CustomUserStore(context.Get<TaskerDbContext>()));

            return manager;
        }

        public override Task<User> FindAsync(string userName, string password)
        {
            var taskInvoke = Task<User>.Factory.StartNew(() =>
            {
                var result = PasswordHasher.VerifyHashedPassword(userName, password);

                return result == PasswordVerificationResult.SuccessRehashNeeded ? Store.FindByNameAsync(userName).Result : null;
            });

            return taskInvoke;
        }
    }
}