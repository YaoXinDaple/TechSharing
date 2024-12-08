using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nullable
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
