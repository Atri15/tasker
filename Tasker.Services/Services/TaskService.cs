using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Tasker.Data.DAL;
using Tasker.Data.Model;
using Tasker.Services.Intefaces;

namespace Tasker.Services.Services
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
                .Include(x => x.ModifedBy)
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
                .Include(x => x.ModifedBy)
                .Where(x => x.AssignedToUser.Id == assignedUserId)
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Task> FindByName(string mask, Guid assignedUserId)
        {
            var query = _dbContext.Tasks
                .Include(x => x.CreatedBy)
                .Include(x => x.AssignedToUser)
                .Include(x => x.ModifedBy)
                .Where(x => x.AssignedToUser.Id == assignedUserId)
                .Where(x => x.Name != null && x.Name.Contains(mask));

            return query.ToList();
        }

        public void Save(Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException("task");
            }

            //var old = _dbContext.Tasks.FirstOrDefault(x => x.Id == task.Id);
            //if (old == null)
            //{
            //    //create
            //    _dbContext.Tasks.Add(task);
            //}
            //else
            //{
            //    //update
            //    _dbContext.Entry(task).State = EntityState.Modified;
            //}

            _dbContext.Entry(task).State = EntityState.Modified;

            _dbContext.SaveChanges();
        }
    }
}