namespace TaskBoard.ViewModels.TasksModels
{
    public class AddCommentToTaskModel
    {
        public int TaskId { get; }
        public string Text { get; set; }

        public AddCommentToTaskModel(int taskId)
        {
            TaskId = taskId;
        }
    }
}
