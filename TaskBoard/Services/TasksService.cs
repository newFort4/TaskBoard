using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskBoard.Enums;
using TaskBoard.Models;
using TaskBoard.ViewModels;

namespace TaskBoard.Services
{
    public class TasksService
    {
        readonly TaskBoardDbContext db;
        readonly UserManager<IdentityUser> userManager;

        public TasksService(TaskBoardDbContext db, UserManager<IdentityUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
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

        public async Task<TaskDetailsModel> GetTaskAsync(int taskId)
        {
            var boardTask = await db.Tasks.FindAsync(taskId);

            var dependentTasks = await db
                .DependentTasks
                .Include(x => x.MainTask)
                .Include(x => x.SubTask)
                .Where(x => x.SubTask.TaskId == boardTask.TaskId)
                .Select(x => x.MainTask)
                .ToListAsync();

            var taskDetailsModels = TaskDetailsModel.ToControllerModel(boardTask);
            taskDetailsModels.DependentTasks = dependentTasks
                .Select(x => TaskDetailsModel.ToControllerModel(x))
                .ToList();

            return taskDetailsModels;
        }

        public IQueryable<BoardTask> GetBoardTasks()
        {
            return db
                .Tasks
                .Include(x => x.AssignedTo)
                .AsNoTracking();
        }

        public async Task<BoardTask[]> GetBoardTasksAsync()
        {
            return await GetBoardTasks()
                .OrderBy(x => x.TaskId)
                .Take(20)
                .ToArrayAsync();
        }

        public async Task<BoardTask[]> GetBoardTasksAsync(TaskSearchModel taskSearchModel)
        {
            var tasks = GetBoardTasks();

            if (!string.IsNullOrEmpty(taskSearchModel.AssignedTo))
            {
                if (taskSearchModel.AssignedTo == "/Unassigned")
                {
                    tasks = tasks.Where(x => x.AssignedTo == null);
                }
                else
                {
                    tasks = tasks.Where(x => x.AssignedTo.Email.Contains(taskSearchModel.AssignedTo));
                }
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

            return await tasks
                .OrderBy(x => x.TaskId)
                .Take(20)
                .ToArrayAsync();
        }

        public async Task CreateTaskAsync(CreateTaskModel createTaskModel)
        {
            var identityUser = !string.IsNullOrEmpty(createTaskModel.AssignedTo) ?
                await userManager.FindByEmailAsync(createTaskModel.AssignedTo) :
                null;

            var task = new BoardTask
            {
                Title = createTaskModel.Title,
                Description = createTaskModel.Description,
                AssignedTo = identityUser,
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

            var identityUser = await db.Users.SingleOrDefaultAsync(x => x.Email == editTaskModel.AssignedTo);

            task.AssignedTo = identityUser;
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

        public async Task AddDependentTaskAsync(int taskId, int dependentTaskId)
        {
            var subTask = await db.Tasks.FindAsync(taskId);
            var mainTask = await db.Tasks.FindAsync(dependentTaskId);

            if (subTask == null || mainTask == null)
            {
                throw new Exception("Task with provided id doesn't exist.");
            }

            var dependentTask = new DependentTask
            {
                MainTask = mainTask,
                SubTask = subTask,
                Relation = Relation.Required
            };

            await db.DependentTasks.AddAsync(dependentTask);
            await db.SaveChangesAsync();
        }
    }
}