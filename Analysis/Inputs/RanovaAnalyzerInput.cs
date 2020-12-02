using CenterSpace.NMath.Core;
using System.Collections.Generic;

namespace OutputAnalyzer.Analysis.Inputs
{
    public class RanovaAnalyzerInput
    {
        public List<string> FileNames { get; set; }
        public DataFrame Data { get; set; }
    }
}
