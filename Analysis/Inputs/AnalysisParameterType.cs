using System.ComponentModel;

namespace OutputAnalyzer.Analysis.Inputs
{
    public enum AnalysisParameterType
    {
        [Description("Alpha")]
        Alpha,
        [Description("Population Mean")]
        PopulationMean,
        [Description("Population Standard Deviation")]
        PopulationStandardDeviation,
        [Description("Hypothesis Type")]
        HypothesisType
    }
}
