using OutputAnalyzer.Analysis.Analyzers;

namespace OutputAnalyzer.Analysis.Factories
{
    public abstract class AnalyzerFactory
    {
        public abstract IReportableAnalyzer Create();
    }
}
