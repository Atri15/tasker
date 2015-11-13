using System;
using Tasker.Data.Model.Enum;

namespace Tasker.Data.Model
{
    public class Job
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime? DateEnd { get; set; }

        public JobStatus Status { get; set; }

        public User AssignedToUser { get; set; }

        public DateTime Created { get; set; }

        public User CreatedBy { get; set; }

        public DateTime? Modifed { get; set; }

        public User ModifedBy { get; set; }
    }
}
