using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tasker.Web.Models
{
    public class TaskModel
    {
        [Required]
        [Display(Name = "Task name")]
        public string Name { get; set; }


    }
}