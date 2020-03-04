using System.Collections.Generic;

namespace TaskBoard.ViewModels
{
    public class BaseResultsModel<T> where T : class
    {
        public int AllResultsCount { get; }
        public List<T> Results { get; }

        public BaseResultsModel(int allResultsCount, List<T> results)
        {
            AllResultsCount = allResultsCount;
            Results = results;
        }
    }
}
