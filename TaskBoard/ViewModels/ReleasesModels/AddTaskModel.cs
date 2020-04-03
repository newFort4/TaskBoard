namespace TaskBoard.ViewModels.ReleasesModels
{
    public class AddTaskModel
    {
        public int ReleaseId { get; }
        public int TaskId { get; set; }

        public AddTaskModel(int releaseId)
        {
            ReleaseId = releaseId;
        }
    }
}
