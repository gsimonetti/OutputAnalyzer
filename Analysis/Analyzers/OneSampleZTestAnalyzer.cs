using CenterSpace.NMath.Core;
using OutputAnalyzer.Analysis.Reports;
using System.Collections.Generic;

namespace OutputAnalyzer.Analysis.Analyzers
{
    public class OneSampleZTestAnalyzer : OneSampleZTest, IReportableAnalyzer
    {
        public string FileName { get; set; }

        public OneSampleZTestAnalyzer(string fileName, IDFColumn data, double populationMean, double populationStandardDeviation)
            : base(data, populationMean, populationStandardDeviation)
        {
            FileName = fileName;
        }

        public WritableAnalysisReport GetAnalysisReport()
        {
            return new WritableAnalysisReport()
            {
                Header = new List<string>()
                {
                    $"File: {FileName}",
                    ToString()
                }
            };
        }
    }
}
