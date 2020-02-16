using System;
using TaskBoard.Enums;

namespace TaskBoard.ViewModels
{
    public class TaskSearchModel
    {
        public string AssignedTo { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? DeadLine { get; set; }
        public Status Status { get; set; }
        public TaskType Type { get; set; }
        public string Title { get; set; }
    }
}
