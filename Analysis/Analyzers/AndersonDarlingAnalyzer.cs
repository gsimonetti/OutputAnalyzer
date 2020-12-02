using CenterSpace.NMath.Core;
using OutputAnalyzer.Analysis.Reports;
using System.Collections.Generic;

namespace OutputAnalyzer.Analysis.Analyzers
{
    public class AndersonDarlingAnalyzer : OneSampleAndersonDarlingTest, IReportableAnalyzer
    {
        public string FileName { get; set; }

        public AndersonDarlingAnalyzer(string fileName, IDFColumn data) : base(data)
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
