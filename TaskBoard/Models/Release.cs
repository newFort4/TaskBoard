using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TaskBoard.Models
{
    public class Release
    {
        [Key]
        public int ReleaseId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public IdentityUser AssignedTo { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}
