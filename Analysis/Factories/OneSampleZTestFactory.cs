using OutputAnalyzer.Analysis.Analyzers;
using OutputAnalyzer.Analysis.Inputs;

namespace OutputAnalyzer.Analysis.Factories
{
    public class OneSampleZTestFactory : AnalyzerFactory
    {
        private readonly OneSampleAnalyzerInput input;

        public OneSampleZTestFactory(OneSampleAnalyzerInput input)
        {
            this.input = input;
        }

        public override IReportableAnalyzer Create()
        {
            if (input?.DataFrame == null)
            {
                return new NullReportableAnalyzer(input.FileName);
            }

            AnalysisParameter populationMeanParameter = input.Parameters.Find(x => x.Type == AnalysisParameterType.PopulationMean);
            AnalysisParameter populationStandardDeviationParameter = input.Parameters.Find(x => x.Type == AnalysisParameterType.PopulationMean);

            bool validPopulationMean = double.TryParse(populationMeanParameter.Value, out double populationMean);
            bool validPopulationStandardDeviation = double.TryParse(populationStandardDeviationParameter.Value, out double populationStandardDeviation);

            if (validPopulationMean && validPopulationStandardDeviation)
            {
                return new OneSampleZTestAnalyzer(input.FileName, input.DataFrame["Values"], populationMean, populationStandardDeviation);
            }

            return new NullReportableAnalyzer(input.FileName);
        }
    }
}
