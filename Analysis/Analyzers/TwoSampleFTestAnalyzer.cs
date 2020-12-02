using CenterSpace.NMath.Core;
using OutputAnalyzer.Analysis.Reports;
using System.Collections.Generic;

namespace OutputAnalyzer.Analysis.Analyzers
{
    public class TwoSampleFTestAnalyzer : TwoSampleFTest, IReportableAnalyzer
    {
        public string FileName { get; set; }

        public TwoSampleFTestAnalyzer(string fileName, IDFColumn dataA, IDFColumn dataB, double alpha, HypothesisType type) : base(dataA, dataB, alpha, type)
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
