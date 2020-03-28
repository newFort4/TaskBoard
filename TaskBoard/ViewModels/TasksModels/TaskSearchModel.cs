using System;
using TaskBoard.Enums;

namespace TaskBoard.ViewModels.TasksModels
{
    public class TaskSearchModel : SearchModel
    {
        public string AssignedTo { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? DeadLine { get; set; }
        public bool AllStatuses { get; set; } = true;
        public Status Status { get; set; }
        public bool AllTypes { get; set; } = true;
        public TaskType? Type { get; set; }
        public string Title { get; set; }
    }
}
