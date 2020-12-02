using OutputAnalyzer.Analysis.Analyzers;
using OutputAnalyzer.Analysis.Inputs;

namespace OutputAnalyzer.Analysis.Factories
{
    public class AndersonDarlingAnalyzerFactory : AnalyzerFactory
    {
        private readonly OneSampleAnalyzerInput input;

        public AndersonDarlingAnalyzerFactory(OneSampleAnalyzerInput input)
        {
            this.input = input;
        }

        public override IReportableAnalyzer Create()
        {
            if (input?.DataFrame == null)
            {
                return new NullReportableAnalyzer(input.FileName);
            }

            return new AndersonDarlingAnalyzer(input.FileName, input.DataFrame["Values"]);
        }
    }
}
