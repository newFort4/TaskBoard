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
        public Release Release { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public TaskType Type { get; set; }
        [Required]
        public Status Status { get; set; }
        [Range(0, 100)]
        [DefaultValue(0)]
        public int Progress { get; set; }
        public string Description { get; set; }
        [DefaultValue(0)]
        public int StoryPoint { get; set; }
        [Required]
        public DateTime Created { get; set; }
        public DateTime? DeadLine { get; set; }
    }
}
