using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Tasker.Data.DAL;
using Tasker.Data.Model;
using Task = System.Threading.Tasks.Task;

namespace Tasker.Data.Store
{
    public class CustomUserStore : IUserStore<User, Guid>
    {
        private TaskerDbContext _dbContext;

        public CustomUserStore(TaskerDbContext taskerDbContext)
        {
            _dbContext = taskerDbContext;
        }

        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
                _dbContext = null;
            }
        }

        public Task CreateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(Guid userId)
        {
            return Task<User>.Factory.StartNew(() =>
            {
                var user = User.TestUser;
                return user.Id == userId ? user : null;
            });
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return Task<User>.Factory.StartNew(() =>
            {
                var user = User.TestUser;
                return String.Compare(user.UserName, userName, StringComparison.InvariantCultureIgnoreCase) == 0 ? user : null;
            });
        }
    }
}
