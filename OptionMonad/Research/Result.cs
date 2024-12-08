namespace OptionMonad.Research
{
    public class Result
    {
        public Result(decimal confidenceScore, decimal reproducibilityFactor)
        {
            ConfidenceScore = confidenceScore;
            ReproducibilityFactor = reproducibilityFactor;
        }

        public decimal ConfidenceScore { get; set; }

        public decimal ReproducibilityFactor { get; set; }
    }
}
