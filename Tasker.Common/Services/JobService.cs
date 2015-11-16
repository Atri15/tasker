using System;
using System.Collections.Generic;
using System.Linq;
using Tasker.Common.Intefaces;
using Tasker.Data.DAL;
using Tasker.Data.Model;

namespace Tasker.Common.Services
{
    public class JobService : IJobService
    {
        private readonly TaskerDbContext _dbContext;

        public JobService(TaskerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Job> GetAll(Guid assignedUserId)
        {
            var query = _dbContext.Jobs
                .Where(x => x.AssignedToUser.Id == assignedUserId)
                .OrderBy(x => x.DateEnd)
                .ThenBy(x => x.Name);

            return query.ToList();
        }

        public Job FindById(Guid id, Guid assignedUserId)
        {
            return _dbContext.Jobs
               .Where(x => x.AssignedToUser.Id == assignedUserId)
               .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Job> FindByName(string mask, Guid assignedUserId)
        {
            var query = _dbContext.Jobs
               .Where(x => x.AssignedToUser.Id == assignedUserId)
               .Where(x => x.Name != null && x.Name.Contains(mask));

            return query.ToList();
        }

        public void Save(Job job)
        {
            throw new NotImplementedException();
        }
    }
}