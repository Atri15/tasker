using System;
using System.ComponentModel.DataAnnotations;
using Tasker.Data.Model.Enum;

namespace Tasker.Web.Models
{
    public class TaskViewModel
    {
        [Display(Name = "Task Id")]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Task name")]
        public string Name { get; set; }

        [Display(Name = "Task end date")]
        public DateTime? DateEnd { get; set; }

        [Required]
        [Display(Name = "Task status")]
        public TaskStatus Status { get; set; }

        public string Color { get; set; }
    }
}