using System;
using TaskBoard.Enums;

namespace TaskBoard.ViewModels
{
    public class EditTaskModel
    {
        public int TaskId { get; set; }
        public string AssignedTo { get; set; }
        public string Title { get; set; }
        public TaskType Type { get; set; }
        public Status Status { get; set; }
        public int Progress { get; set; }
        public string Description { get; set; }
        public DateTime? DeadLine { get; set; }
    }
}