using OutputAnalyzer.Analysis.Analyzers;
using OutputAnalyzer.Analysis.Inputs;

namespace OutputAnalyzer.Analysis.Factories
{
    public class OneWayAnovaFactory : AnalyzerFactory
    {
        private readonly AnovaAnalyzerInput input;

        public OneWayAnovaFactory(AnovaAnalyzerInput input)
        {
            this.input = input;
        }

        public override IReportableAnalyzer Create()
        {
            if (input?.Data == null)
            {
                return new NullReportableAnalyzer(string.Join(", ", input.FileNames));
            }

            return new OneWayAnovaAnalyzer(string.Join(", ", input.FileNames), input.Data);
        }
    }
}
