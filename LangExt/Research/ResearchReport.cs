namespace Nullable.Research
{
    public class ResearchReport
    {
        public string ProjectName { get; set; } = string.Empty;

        public List<string>? Stages { get; set; }

        public decimal? LatestConfidenceScore { get; set; }

        public ExperimentTrend? Trend { get; set; }

        public string? MostSuccessfulExperiment { get; set; }

        public ProjectStatus OverallStatus { get; set; }

        public int ExperimentCount { get; set; }
    }











}
