using OutputAnalyzer.Analysis.Reports;
using System.Collections.Generic;

namespace OutputAnalyzer.Analysis.Analyzers
{
    public class NullReportableAnalyzer : IReportableAnalyzer
    {
        public string FileName { get; set; }

        private readonly string status;

        public NullReportableAnalyzer(string fileName)
        {
            FileName = fileName;
            status = IReportableAnalyzer.StatusFailed;
        }

        public WritableAnalysisReport GetAnalysisReport()
        {
            return new WritableAnalysisReport()
            {
                Header = new List<string>()
                {
                    $"File: {FileName}"
                },
                Results = new List<NumericResult>(),
                Footer = new List<string>()
                {
                    $"Status: {status}"
                }
            };
        }
    }
}
