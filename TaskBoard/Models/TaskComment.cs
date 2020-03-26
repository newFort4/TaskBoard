using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TaskBoard.Models
{
    public class TaskComment
    {
        [Key]
        public int TaskCommentId { get; set; }
        [Required]
        [MaxLength(250)]
        public string Text { get; set; }
        [Required]
        public IdentityUser IdentityUser { get; set; }
        [Required]
        public BoardTask Task { get; set; }
        public DateTime Created { get; set; }
    }
}
