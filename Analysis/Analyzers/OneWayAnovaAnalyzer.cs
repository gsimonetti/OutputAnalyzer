using CenterSpace.NMath.Core;
using OutputAnalyzer.Analysis.Reports;
using System.Collections.Generic;

namespace OutputAnalyzer.Analysis.Analyzers
{
    public class OneWayAnovaAnalyzer : OneWayAnova, IReportableAnalyzer
    {
        public string FileName { get; set; }

        private readonly List<int> columnPositions = new List<int>() { 0, 18, 40, 58, 73, 83 };

        public OneWayAnovaAnalyzer(string fileName, DoubleVector[] data) : base(data)
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
                    string.Empty,
                    "Analysis of variance",
                    "----------------------------",
                    string.Empty,
                    TableFormatter.BuildTableRow(columnPositions, new List<string>()
                    {
                        "Source",
                        "Degrees of Freedom",
                        "Sum Of Squares",
                        "Mean Square",
                        "F",
                        "P"
                    }),
                    TableFormatter.BuildTableRow(columnPositions, new List<string>()
                    {
                        "Between groups",
                        AnovaTable.DegreesOfFreedomBetween.ToString("0.####"),
                        AnovaTable.SumOfSquaresBetween.ToString("0.####"),
                        AnovaTable.MeanSquareBetween.ToString("0.####"),
                        AnovaTable.FStatistic.ToString("0.####"),
                        AnovaTable.FStatisticPValue.ToString("0.####")
                    }),
                    TableFormatter.BuildTableRow(columnPositions, new List<string>()
                    {
                        "Within groups",
                        AnovaTable.DegreesOfFreedomWithin.ToString("0.####"),
                        AnovaTable.SumOfSquaresWithin.ToString("0.####"),
                        AnovaTable.MeanSquareWithin.ToString("0.####"),
                        ".",
                        "."
                    }),
                    TableFormatter.BuildTableRow(columnPositions, new List<string>()
                    {
                        "Total",
                        AnovaTable.DegreesOfFreedomTotal.ToString("0.####"),
                        AnovaTable.SumOfSquaresTotal.ToString("0.####"),
                        AnovaTable.MeanSquareTotal.ToString("0.####"),
                        ".",
                        "."
                    })
                }
            };
        }
    }
}
