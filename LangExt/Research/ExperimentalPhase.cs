using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nullable
{
    public class ExperimentalPhase
    {
        public ExperimentalPhase(string phaseName, Experiment? latestExperiment, List<Experiment> historicalExperiments)
        {
            PhaseName = phaseName;
            LatestExperiment = latestExperiment;
            HistoricalExperiments = historicalExperiments;
        }

        public string PhaseName { get; set; }
        public Experiment? LatestExperiment { get; set; }
        public List<Experiment> HistoricalExperiments { get; set; }
    }
}
