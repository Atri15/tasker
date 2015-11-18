using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Tasker.Data.DAL;
using Tasker.Data.Interfaces;
using Tasker.Data.Model;

namespace Tasker.Data.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskerDbContext _dbContext;

        public TaskService(TaskerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Task> GetAll(Guid assignedUserId)
        {
            var query = _dbContext.Tasks
                .Include(x => x.CreatedBy)
                .Include(x => x.AssignedToUser)
                .Where(x => x.AssignedToUser.Id == assignedUserId)
                .OrderBy(x => x.DateEnd)
                .ThenBy(x => x.Name);

            return query.ToList();
        }

        public Task FindById(Guid id, Guid assignedUserId)
        {
            return _dbContext.Tasks
                .Include(x => x.CreatedBy)
                .Include(x => x.AssignedToUser)
                .Where(x => x.AssignedToUser.Id == assignedUserId)
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Task> FindByName(string mask, Guid assignedUserId)
        {
            var query = _dbContext.Tasks
                .Include(x => x.CreatedBy)
                .Include(x => x.AssignedToUser)
                .Where(x => x.AssignedToUser.Id == assignedUserId);
            if (!String.IsNullOrWhiteSpace(mask))
            {
                query = query.Where(x => x.Name != null && x.Name.Contains(mask));
            }

            query = query.OrderBy(x => x.DateEnd).ThenBy(x => x.Name);

            return query.ToList();
        }

        public void Save(Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException("task");
            }

            //save task
            if (task.Id == Guid.Empty)
            {
                //create
                task.Id = Guid.NewGuid();
                if (task.AssignedToUser != null) _dbContext.Entry(task.AssignedToUser).State = EntityState.Unchanged;
                if (task.CreatedBy != null) _dbContext.Entry(task.CreatedBy).State = EntityState.Unchanged;

                _dbContext.Tasks.Add(task);
            }
            else
            {
                //update
                if (task.AssignedToUser != null) _dbContext.Entry(task.AssignedToUser).State = EntityState.Modified;
                if (task.CreatedBy != null) _dbContext.Entry(task.CreatedBy).State = EntityState.Modified;

                _dbContext.Entry(task).State = EntityState.Modified;
            }

            _dbContext.SaveChanges();
        }
    }
}