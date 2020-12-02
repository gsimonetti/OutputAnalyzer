using OutputAnalyzer.Analysis.Analyzers;
using OutputAnalyzer.Analysis.Inputs;

namespace OutputAnalyzer.Analysis.Factories
{
    public class OneSampleTTestAnalyzerFactory : AnalyzerFactory
    {
        private readonly OneSampleAnalyzerInput input;

        public OneSampleTTestAnalyzerFactory(OneSampleAnalyzerInput input)
        {
            this.input = input;
        }

        public override IReportableAnalyzer Create()
        {
            if (input?.DataFrame == null)
            {
                return new NullReportableAnalyzer(input.FileName);
            }

            if (input.Parameters.Find(x => x.Type == AnalysisParameterType.PopulationMean) is AnalysisParameter parameter)
            {
                if (double.TryParse(parameter.Value, out double value))
                {
                    return new OneSampleTTestAnalyzer(input.FileName, input.DataFrame["Values"], value);
                }
            }

            return new NullReportableAnalyzer(input.FileName);
        }
    }
}
