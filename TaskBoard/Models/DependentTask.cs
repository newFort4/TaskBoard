using TaskBoard.Enums;

namespace TaskBoard.Models
{
    public class DependentTask
    {
        public int DependentTaskId { get; set; }
        public BoardTask MainTask { get; set; }
        public BoardTask SubTask { get; set; }
        public Relation Relation { get; set; }
    }
}
