using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Tasker.Data.Model;
using Tasker.Data.Model.Enum;

namespace Tasker.Data.DAL
{
    public class TaskerDbInitializer : DropCreateDatabaseIfModelChanges<TaskerDbContext>
    {
        protected override void Seed(TaskerDbContext context)
        {
            var users = new List<User>
            {
                User.TestUser,
                User.AdminUser
            };

            users.ForEach(x => context.Users.Add(x));
            context.SaveChanges();

            var jobs = new List<Job>
            {
                new Job
                {
                    Id = Guid.NewGuid(),
                    AssignedToUser = User.TestUser,
                    Created = DateTime.UtcNow,
                    CreatedBy = User.AdminUser,
                    DateEnd = DateTime.UtcNow.AddDays(3),
                    Modifed = null,
                    ModifedBy = null,
                    Name = "First task",
                    Status = JobStatus.New
                },
                new Job
                {
                    Id = Guid.NewGuid(),
                    AssignedToUser = User.TestUser,
                    Created = DateTime.UtcNow,
                    CreatedBy = User.AdminUser,
                    DateEnd = DateTime.UtcNow.AddDays(2),
                    Modifed = null,
                    ModifedBy = null,
                    Name = "Second task",
                    Status = JobStatus.New
                },
                new Job
                {
                    Id = Guid.NewGuid(),
                    AssignedToUser = User.TestUser,
                    Created = DateTime.UtcNow.AddDays(-2),
                    CreatedBy = User.AdminUser,
                    DateEnd = DateTime.UtcNow.Date,
                    Modifed = null,
                    ModifedBy = null,
                    Name = "Finished task",
                    Status = JobStatus.Done
                },
            };
            jobs.AddRange(Enumerable.Range(4, 100).Select(x => new Job()
            {
                Id = Guid.NewGuid(),
                Created = DateTime.UtcNow,
                CreatedBy = User.AdminUser,
                DateEnd = DateTime.UtcNow.AddDays(x - 50),
                AssignedToUser = User.TestUser,
                Modifed = null,
                ModifedBy = null,
                Name = String.Format("Task #{0:000}", x),
                Status = JobStatus.New
            }));

            jobs.ForEach(x => context.Jobs.Add(x));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}