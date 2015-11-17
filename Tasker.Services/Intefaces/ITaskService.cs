using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Data.Model;
using Task = Tasker.Data.Model.Task;

namespace Tasker.Common.Intefaces
{
    public interface ITaskService
    {
        IEnumerable<Task> GetAll(Guid assignedUserId);

        Task FindById(Guid id, Guid assignedUserId);

        IEnumerable<Task> FindByName(string mask, Guid assignedUserId);

        void Save(Task task);
    }
}
