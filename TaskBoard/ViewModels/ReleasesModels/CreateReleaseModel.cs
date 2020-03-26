using System;
using System.ComponentModel.DataAnnotations;
using TaskBoard.Enums;

namespace TaskBoard.ViewModels.Releases
{
    public class CreateReleaseModel
    {
        [Required]
        [StringLength(32)]
        public string Title { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public string AssignedTo { get; set; }

        public TaskType Type { get; set; }

        public DateTime? ReleaseDate { get; set; }
    }
}
