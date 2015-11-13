using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Data.Model;

namespace Tasker.Common.Intefaces
{
    public interface IJobService
    {
        IEnumerable<Job> GetAll(User assignedUser);

        Job FindById(Guid id, User assignedUser);

        IEnumerable<Job> FindByName(string mask, User assignedUser);

        void Save(Job job);
    }
}
