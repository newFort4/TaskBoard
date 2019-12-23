using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskBoard.Enums;
using TaskBoard.Models;
using TaskBoard.ViewModels;

namespace TaskBoard.Services
{
    public class TasksService
    {
        readonly TaskBoardDbContext db;

        public TasksService(TaskBoardDbContext db)
        {
            this.db = db;
        }

        public async Task<BoardTask[]> GetBoardTasksAsync()
        {
            return await db.Tasks.ToArrayAsync();
        }

        public async Task CreateAsync(CreateTaskModel createTaskModel)
        {
            var task = new BoardTask
            {
                Title = createTaskModel.Title,
                Description = createTaskModel.Description,
                AssignedTo = createTaskModel.AssignedTo,
                Status = Status.New,
                Type = createTaskModel.Type,
                Created = DateTime.Now,
                DeadLine = createTaskModel.DeadLine
            };

            await db.Tasks.AddAsync(task);
            await db.SaveChangesAsync();
        }

        public async Task RemoveTaskAsync(BoardTask task)
        {
            db.Tasks.Remove(await db.Tasks.FindAsync(task.TaskId));
            await db.SaveChangesAsync();
        }
    }
}