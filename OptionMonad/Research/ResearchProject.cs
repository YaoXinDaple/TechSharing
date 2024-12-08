using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionMonad.Research
{
    public class ResearchProject
    {
        public ResearchProject(string name, ExperimentalPhase? experimentalPhase)
        {
            Name = name;
            ExperimentalPhase = experimentalPhase;
        }

        public string Name { get; set; }

        public ExperimentalPhase? ExperimentalPhase { get; set; }
    }
}
