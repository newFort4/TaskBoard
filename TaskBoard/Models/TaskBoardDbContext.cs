using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TaskBoard.Models
{
    public class TaskBoardDbContext: IdentityDbContext
    {
        public DbSet<Release> Releases { get; set; }
        public DbSet<BoardTask> Tasks { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }
        public DbSet<DependentTask> DependentTasks { get; set; }

        public TaskBoardDbContext(DbContextOptions<TaskBoardDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
