using CenterSpace.NMath.Core;
using System.Collections.Generic;

namespace OutputAnalyzer.Analysis.Inputs
{
    public class TwoSampleAnalyzerInput
    {
        public string FileNameA { get; set; }
        public string FileNameB { get; set; }
        public DataFrame DataFrameA { get; set; }
        public DataFrame DataFrameB { get; set; }
        public List<AnalysisParameter> Parameters { get; set; }
    }
}
