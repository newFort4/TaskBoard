namespace TaskBoard.ViewModels
{
    public class SearchModel
    {
        public int PageSize { get; set; } = 10;
        public int CurrentPage { get; set; } = 1;
        public string SortBy { get; set; }
        public bool Reverse { get; set; }
    }
}
