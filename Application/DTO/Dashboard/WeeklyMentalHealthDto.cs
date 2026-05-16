namespace Application.DTO.Dashboard
{
    // ── Weekly trend chart ───────────────────────────────────────────────────

    public class WeeklyMentalHealthDto
    {
        /// <summary>Display label, e.g. "Week 1".</summary>
        public string WeekLabel         { get; set; } = string.Empty;

        /// <summary>Monday (start) of this ISO week.</summary>
        public DateTime WeekStart       { get; set; }

        public double AvgDepressionScore { get; set; }
        public double AvgAnxietyScore    { get; set; }
        public double AvgStressScore     { get; set; }
    }
}
