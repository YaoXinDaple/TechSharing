namespace OptionMonad.Research
{
    public class Experiment
    {
        public Experiment(string experimentName, Result? result)
        {
            ExperimentName = experimentName;
            Result = result;
        }

        public string ExperimentName { get; set; }
        public Result? Result { get; set; }
    }
}
