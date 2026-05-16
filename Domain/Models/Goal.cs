using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Goal
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Priority GoalPriority  { get; set; }

        public DateOnly? Deadline { get; set; }

        public bool IsCompleted { get; set; } = false; 
        public DateOnly? DoneAt { get; set; }

        // Navigation
        public List<ChecklistItem> Items { get; set; }
        public Student Student { get; set; }

        [NotMapped]
        public int Progress
        {
            get
            { 
                return CalculateProgress();
            }

            set => CalculateProgress();
        } 

        private int CalculateProgress()
        {
            if (Items == null || Items.Count == 0)
                return 0;

            var completed = Items.Count(i => i.IsCompleted);
            return (int)((double)completed / Items.Count * 100);
        }
    }
    public enum Priority
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
}