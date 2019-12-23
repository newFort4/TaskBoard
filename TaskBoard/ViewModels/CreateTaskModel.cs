using System;
using System.ComponentModel.DataAnnotations;
using TaskBoard.Enums;

namespace TaskBoard.ViewModels
{
    public class CreateTaskModel
    {
        [Required]
        [StringLength(32)]
        public string Title { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        [Required]
        public string AssignedTo { get; set; }

        public TaskType Type { get; set; }

        public DateTime? DeadLine { get; set; }
    }
}
