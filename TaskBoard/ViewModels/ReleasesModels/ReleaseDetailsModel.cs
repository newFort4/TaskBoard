using System;
using TaskBoard.Enums;
using TaskBoard.Models;

namespace TaskBoard.ViewModels.Releases
{
    public class ReleaseDetailsModel
    {
        public int ReleaseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public DateTime? ReleaseDate { get; set; }

        public static ReleaseDetailsModel ToControllerModel(Release release)
        {
            return new ReleaseDetailsModel
            {
                ReleaseId = release.ReleaseId,
                Title = release.Title,
                Description = release.Description,
                AssignedTo = release.AssignedTo?.Email,
                ReleaseDate = release.ReleaseDate
            };
        }
    }
}
