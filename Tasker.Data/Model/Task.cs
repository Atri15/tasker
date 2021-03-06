﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tasker.Data.Model.Enum;

namespace Tasker.Data.Model
{
    public class Task
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime? DateEnd { get; set; }

        [Required]
        public TaskStatus? Status { get; set; }

        [Required]
        public User AssignedToUser { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public User CreatedBy { get; set; }
    }
}