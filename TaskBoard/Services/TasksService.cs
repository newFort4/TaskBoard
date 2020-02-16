using System;
using System.Linq;
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

        public async Task<EditTaskModel> GetTaskForEditAsync(int taskId)
        {
            var task = await GetTaskAsync(taskId);

            return new EditTaskModel
            {
                TaskId = task.TaskId,
                AssignedTo = task.AssignedTo,
                Title = task.Title,
                Type = task.Type,
                Status = task.Status,
                Description = task.Description,
                DeadLine = task.DeadLine
            };
        }

        public async Task<BoardTask> GetTaskAsync(int taskId)
        {
            return await db.Tasks.FindAsync(taskId);
        }

        public async Task<BoardTask[]> GetBoardTasksAsync()
        {
            return await db.Tasks.ToArrayAsync();
        }

        public async Task<BoardTask[]> GetBoardTasksAsync(TaskSearchModel taskSearchModel)
        {
            var tasks = db.Tasks.AsNoTracking();

            if (!string.IsNullOrEmpty(taskSearchModel.AssignedTo))
            {
                tasks = tasks.Where(x => x.AssignedTo == taskSearchModel.AssignedTo);
            }

            if (!string.IsNullOrEmpty(taskSearchModel.Title))
            {
                tasks = tasks.Where(x => x.Title.Contains(taskSearchModel.Title));
            }

            if (taskSearchModel.DeadLine.HasValue)
            {
                tasks = tasks.Where(x => x.DeadLine < DateTime.Now);
            }

            tasks = tasks.Where(x => x.Status == taskSearchModel.Status);
            tasks = tasks.Where(x => x.Type == taskSearchModel.Type);

            return await tasks.ToArrayAsync();
        }

        public async Task CreateTaskAsync(CreateTaskModel createTaskModel)
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

        public async Task EditTaskAsync(EditTaskModel editTaskModel)
        {
            var task = await db.Tasks.FindAsync(editTaskModel.TaskId);

            task.AssignedTo = editTaskModel.AssignedTo;
            task.Title = editTaskModel.Title;
            task.Type = editTaskModel.Type;
            task.Status = editTaskModel.Status;
            task.Description = editTaskModel.Description;
            task.DeadLine = editTaskModel.DeadLine;

            await db.SaveChangesAsync();
        }

        public async Task RemoveTaskAsync(int taskId)
        {
            db.Tasks.Remove(await db.Tasks.FindAsync(taskId));
            await db.SaveChangesAsync();
        }
    }
}