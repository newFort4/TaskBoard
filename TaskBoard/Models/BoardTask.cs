using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using TaskBoard.Enums;

namespace TaskBoard.Models
{
    public class BoardTask
    {
        [Key]
        public int TaskId { get; set; }
        //public IdentityUser AssignedTo { get; set; }
        public string AssignedTo { get; set; }
        public string Title { get; set; }
        public TaskType Type { get; set; }
        public Status Status { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? DeadLine { get; set; }
    }
}
