using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TaskBoard.Enums;

namespace TaskBoard.ViewModels.TasksModels
{
    public class CreateTaskModel
    {
        [Required]
        [StringLength(32)]
        public string Title { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        [DefaultValue(0)]
        public int StoryPoint { get; set; }

        public string AssignedTo { get; set; }

        public TaskType Type { get; set; }

        public DateTime? DeadLine { get; set; }
    }
}
