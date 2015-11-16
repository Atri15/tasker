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
        IEnumerable<Job> GetAll(Guid assignedUserId);

        Job FindById(Guid id, Guid assignedUserId);

        IEnumerable<Job> FindByName(string mask, Guid assignedUserId);

        void Save(Job job);
    }
}
