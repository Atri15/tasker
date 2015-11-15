using Microsoft.AspNet.Identity;

namespace Tasker.Data.Manager
{
    public class CustomPasswordHasher : PasswordHasher
    {
        public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            return PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}