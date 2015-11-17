using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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
        public virtual IDbSet<Task> Tasks { get; set; }

        public static TaskerDbContext Create()
        {
            return new TaskerDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}