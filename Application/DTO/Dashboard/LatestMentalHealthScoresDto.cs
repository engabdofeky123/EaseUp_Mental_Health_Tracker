namespace Application.DTO.Dashboard
{
    // ── Summary cards ────────────────────────────────────────────────────────

    public class LatestMentalHealthScoresDto
    {
        public double DepressionScore { get; set; }
        public double AnxietyScore    { get; set; }
        public double StressScore     { get; set; }
        public string Diagnosis       { get; set; } = string.Empty;
        public DateTime RecordedAt    { get; set; }
    }
}
