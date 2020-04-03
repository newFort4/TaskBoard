using System;
using System.Collections.Generic;
using System.Linq;
using TaskBoard.Models;
using TaskBoard.ViewModels.TasksModels;

namespace TaskBoard.ViewModels.ReleasesModels
{
    public class ReleaseDetailsModel
    {
        public int ReleaseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public IEnumerable<TaskDetailsModel> Tasks { get; set; }

        public static ReleaseDetailsModel ToControllerModel(Release release, IEnumerable<BoardTask> boardTasks)
        {
            return new ReleaseDetailsModel
            {
                ReleaseId = release.ReleaseId,
                Title = release.Title,
                Description = release.Description,
                AssignedTo = release.AssignedTo?.Email,
                ReleaseDate = release.ReleaseDate,
                Tasks = boardTasks.Select(x => TaskDetailsModel.ToControllerModel(x))
            };
        }
    }
}
