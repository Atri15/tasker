using System;
using System.Collections.Generic;
using Tasker.Common.Intefaces;
using Tasker.Data.Model;

namespace Tasker.Common.Services
{
    public class JobService : IJobService
    {
        public IEnumerable<Job> GetAll(User assignedUser)
        {
            throw new NotImplementedException();
        }

        public Job FindById(Guid id, User assignedUser)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Job> FindByName(string mask, User assignedUser)
        {
            throw new NotImplementedException();
        }

        public void Save(Job job)
        {
            throw new NotImplementedException();
        }
    }
}