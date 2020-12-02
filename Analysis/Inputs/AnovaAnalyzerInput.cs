using CenterSpace.NMath.Core;
using System.Collections.Generic;

namespace OutputAnalyzer.Analysis.Inputs
{
    public class AnovaAnalyzerInput
    {
        public List<string> FileNames { get; set; }
        public DoubleVector[] Data { get; set; }
    }
}
