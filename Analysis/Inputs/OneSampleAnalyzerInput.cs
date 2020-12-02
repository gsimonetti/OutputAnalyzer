using CenterSpace.NMath.Core;
using System.Collections.Generic;

namespace OutputAnalyzer.Analysis.Inputs
{
    public class OneSampleAnalyzerInput
    {
        public string FileName { get; set; }
        public DataFrame DataFrame { get; set; }
        public List<AnalysisParameter> Parameters { get; set; }
    }
}
