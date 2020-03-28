using System;

namespace TaskBoard.ViewModels.ReleasesModels
{
    public class ReleaseSearchModel : SearchModel
    {
        public string Title { get; set; }
        public string AssignedTo { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}
