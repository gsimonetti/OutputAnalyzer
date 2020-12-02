using CenterSpace.NMath.Core;
using OutputAnalyzer.Analysis.Reports;
using System.Collections.Generic;

namespace OutputAnalyzer.Analysis.Analyzers
{
    public class OneSampleTTestAnalyzer : OneSampleTTest, IReportableAnalyzer
    {
        public string FileName { get; set; }

        public OneSampleTTestAnalyzer(string fileName, IDFColumn data, double populationMean) : base(data, populationMean)
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
