namespace Domain.Models
{
    public class MoodEntries
    {
            public int Id { get; set; }
            public int StudentId { get; set; }

            public string Content { get; set; }
            public DateTime CreatedAt { get; set; }
            public string MoodResult { get; set; }  // From Mode Analysis Model

            // Navigation
            public Student Student { get; set; }
    }
}