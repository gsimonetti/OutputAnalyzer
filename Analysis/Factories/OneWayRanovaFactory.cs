using OutputAnalyzer.Analysis.Analyzers;
using OutputAnalyzer.Analysis.Inputs;

namespace OutputAnalyzer.Analysis.Factories
{
    public class OneWayRanovaFactory : AnalyzerFactory
    {
        private readonly RanovaAnalyzerInput input;

        public OneWayRanovaFactory(RanovaAnalyzerInput input)
        {
            this.input = input;
        }

        public override IReportableAnalyzer Create()
        {
            if (input?.Data == null)
            {
                return new NullReportableAnalyzer(string.Join(", ", input.FileNames));
            }

            return new OneWayRanovaAnalyzer(string.Join(", ", input.FileNames), input.Data);
        }
    }
}
