using System.Data.Entity;
using Tasker.Data.Model;

namespace Tasker.Data.DAL
{
    public class TaskerDbContext : DbContext
    {
        public TaskerDbContext()
            : base("DefaultConnection")
        {
        }

        public virtual IDbSet<User> Users { get; set; }
        public virtual IDbSet<Job> Jobs { get; set; }

        public static TaskerDbContext Create()
        {
            return new TaskerDbContext();
        }
    }
}