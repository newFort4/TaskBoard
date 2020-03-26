using System;

namespace TaskBoard.ViewModels.Releases
{
    public class EditReleaseModel
    {
        public int ReleaseId { get; set; }
        public string AssignedTo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}