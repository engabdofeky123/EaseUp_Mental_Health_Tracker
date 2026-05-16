namespace Application.DTO.Dashboard
{
    // ── Sentiment heatmap ────────────────────────────────────────────────────

    public class WeeklySentimentDto
    {
        public string WeekLabel   { get; set; } = string.Empty;

        public DateTime WeekStart { get; set; }
        
        public Dictionary<string, string?> DailyMoods { get; set; } = new();
    }
}
