namespace OutputAnalyzer.Analysis.Inputs
{
    public class AnalysisParameter
    {
        public AnalysisParameterType Type { get; set; }
        public string Value { get; set; }

        public AnalysisParameter(AnalysisParameterType type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}
