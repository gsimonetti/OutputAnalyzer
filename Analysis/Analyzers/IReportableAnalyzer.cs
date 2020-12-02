using OutputAnalyzer.Analysis.Reports;

namespace OutputAnalyzer.Analysis.Analyzers
{
    public interface IReportableAnalyzer
    {
        public const string StatusSuccess = "SUCCESS";
        public const string StatusFailed = "FAILED";

        string FileName { get; set; }

        WritableAnalysisReport GetAnalysisReport();
    }
}
