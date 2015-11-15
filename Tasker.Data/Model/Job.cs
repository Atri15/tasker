using System;
using System.ComponentModel.DataAnnotations;
using Tasker.Data.Model.Enum;

namespace Tasker.Data.Model
{
    public class Job
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime? DateEnd { get; set; }

        [Required]
        public JobStatus? Status { get; set; }

        [Required]
        public User AssignedToUser { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public User CreatedBy { get; set; }

        public DateTime? Modifed { get; set; }

        public User ModifedBy { get; set; }
    }
}
