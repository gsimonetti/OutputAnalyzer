using CenterSpace.NMath.Core;
using OutputAnalyzer.Analysis.Analyzers;
using OutputAnalyzer.Analysis.Inputs;

namespace OutputAnalyzer.Analysis.Factories
{
    public class TwoSamplePairedTTestFactory : AnalyzerFactory
    {
        private readonly TwoSampleAnalyzerInput input;

        public TwoSamplePairedTTestFactory(TwoSampleAnalyzerInput input)
        {
            this.input = input;
        }

        public override IReportableAnalyzer Create()
        {
            if (input?.DataFrameA == null || input?.DataFrameB == null)
            {
                return new NullReportableAnalyzer($"{input.FileNameA} and {input.FileNameB}");
            }

            AnalysisParameter hypothesisTypeParameter = input.Parameters.Find(x => x.Type == AnalysisParameterType.HypothesisType);
            AnalysisParameter alphaParameter = input.Parameters.Find(x => x.Type == AnalysisParameterType.Alpha);

            if (hypothesisTypeParameter == null || alphaParameter == null)
            {
                return new NullReportableAnalyzer($"{input.FileNameA} and {input.FileNameB}");
            }

            string hypothesisTypeName = hypothesisTypeParameter.Value;
            bool isAlphaValid = double.TryParse(alphaParameter.Value, out double alpha);

            if (!isAlphaValid)
            {
                return new NullReportableAnalyzer($"{input.FileNameA} and {input.FileNameB}");
            }

            HypothesisType hypothesisType = HypothesisTypeCreator.FromString(hypothesisTypeName);

            return new TwoSamplePairedTTestAnalyzer($"{input.FileNameA} and {input.FileNameB}", input.DataFrameA["Values"], input.DataFrameB["Values"], alpha, hypothesisType);
        }
    }
}
