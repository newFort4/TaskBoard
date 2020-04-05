﻿using System;
using System.Collections.Generic;
using TaskBoard.Enums;
using TaskBoard.Models;
using TaskBoard.ViewModels.ReleasesModels;

namespace TaskBoard.ViewModels.TasksModels
{
    public class TaskDetailsModel
    {
        public int TaskId { get; set; }
        public ReleaseDetailsModel Release { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public int StoryPoint { get; set; }
        public TaskType Type { get; set; }
        public Status Status { get; set; }
        public int Progress { get; set; }
        public DateTime Created { get; set; }
        public DateTime? DeadLine { get; set; }
        public List<TaskDetailsModel> DependentTasks { get; set; }
        public List<TaskComment> TaskComments { get; set; }

        public static TaskDetailsModel ToControllerModel(BoardTask task)
        {
            return new TaskDetailsModel
            {
                TaskId = task.TaskId,
                Release = ReleaseDetailsModel.ToControllerModel(task.Release, null),
                Title = task.Title,
                Description = task.Description,
                AssignedTo = task.AssignedTo?.Email,
                Type = task.Type,
                StoryPoint = task.StoryPoint,
                Status = task.Status,
                Progress = task.Progress,
                Created = task.Created,
                DeadLine = task.DeadLine
            };
        }
    }
}
