using CenterSpace.NMath.Core;
using OutputAnalyzer.Analysis.Reports;
using System.Collections.Generic;

namespace OutputAnalyzer.Analysis.Analyzers
{
    public class OneWayRanovaAnalyzer : OneWayRanova, IReportableAnalyzer
    {
        public string FileName { get; set; }

        private readonly List<int> columnPositions = new List<int>() { 0, 18, 40, 58, 73, 83 };

        public OneWayRanovaAnalyzer(string fileName, DataFrame data) : base(data)
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
                    "Repeated measures analysis of variance",
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
                        "Subjects",
                        RanovaTable.DegreesOfFreedomWithinSubject.ToString("0.####"),
                        RanovaTable.SumOfSquaresWithinSubject.ToString("0.####"),
                        RanovaTable.MeanSquareWithinSubject.ToString("0.####"),
                        ".",
                        "."
                    }),
                    TableFormatter.BuildTableRow(columnPositions, new List<string>()
                    {
                        "Treatments",
                        RanovaTable.DegreesOfFreedomTreatment.ToString("0.####"),
                        RanovaTable.SumOfSquaresTreatment.ToString("0.####"),
                        RanovaTable.MeanSquareTreatment.ToString("0.####"),
                        RanovaTable.FStatistic.ToString("0.####"),
                        RanovaTable.FStatisticPValue.ToString("0.####")
                    }),
                    TableFormatter.BuildTableRow(columnPositions, new List<string>()
                    {
                        "Error",
                        RanovaTable.DegreesOfFreedomError.ToString("0.####"),
                        RanovaTable.SumOfSquaresError.ToString("0.####"),
                        RanovaTable.MeanSquareError.ToString("0.####"),
                        ".",
                        "."
                    }),
                    TableFormatter.BuildTableRow(columnPositions, new List<string>()
                    {
                        "Total",
                        RanovaTable.DegreesOfFreedomTotal.ToString("0.####"),
                        RanovaTable.SumOfSquaresTotal.ToString("0.####"),
                        RanovaTable.MeanSquareTotal.ToString("0.####"),
                        ".",
                        "."
                    })
                }
            };
        }
    }
}
