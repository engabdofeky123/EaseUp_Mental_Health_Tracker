namespace Application.DTO.Dashboard
{
    /// <summary>
    /// Top-level response for the student dashboard overview page.
    /// </summary>
    public class DashboardDto
    {
        /// <summary>
        /// Latest mental-health scores shown in the three summary cards.
        /// </summary>
        public LatestMentalHealthScoresDto LatestScores { get; set; } = new();

        /// <summary>
        /// Weekly breakdown used for the multi-line stress/anxiety/depression chart.
        /// Each item represents one calendar week (Week 1 … Week N).
        /// </summary>
        public List<WeeklyMentalHealthDto> WeeklyTrends { get; set; } = new();

        /// <summary>
        /// Sentiment heatmap: one entry per calendar week.
        /// Each entry contains the mood result for every day-of-week (S/M/T/W/T/F/S).
        /// </summary>
        public List<WeeklySentimentDto> SentimentOverTime { get; set; } = new();
    }
}
