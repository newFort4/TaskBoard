using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using TaskBoard.Enums;

namespace TaskBoard.Models
{
    public class BoardTask
    {
        [Key]
        public int TaskId { get; set; }
        public IdentityUser AssignedTo { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public TaskType Type { get; set; }
        [Required]
        public Status Status { get; set; }
        [Required]
        [Range(0, 100)]
        [DefaultValue(0)]
        public int Progress { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime Created { get; set; }
        public DateTime? DeadLine { get; set; }
    }
}
