using System;
using System.Collections.Generic;
using Task = Tasker.Data.Model.Task;

namespace Tasker.Services.Intefaces
{
    public interface ITaskService
    {
        IEnumerable<Task> GetAll(Guid assignedUserId);

        Task FindById(Guid id, Guid assignedUserId);

        IEnumerable<Task> FindByName(string mask, Guid assignedUserId);

        void Save(Task task);
    }
}
