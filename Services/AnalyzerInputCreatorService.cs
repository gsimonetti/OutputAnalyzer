using CenterSpace.NMath.Core;
using OutputAnalyzer.Analysis.Inputs;
using OutputAnalyzer.Models;
using OutputAnalyzer.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace OutputAnalyzer.Services
{
    public static class AnalyzerInputCreatorService
    {
        public static OneSampleAnalyzerInput Create(OneParameterSampleViewModel viewModel, DataFrame dataFrame)
        {
            return new OneSampleAnalyzerInput()
            {
                DataFrame = dataFrame,
                FileName = viewModel.FileName,
                Parameters = new List<AnalysisParameter>
                {
                    new AnalysisParameter(viewModel.FirstParameter, viewModel.ParameterValue)
                }
            };
        }

        public static OneSampleAnalyzerInput Create(TwoParameterSampleViewModel viewModel, DataFrame dataFrame)
        {
            return new OneSampleAnalyzerInput()
            {
                DataFrame = dataFrame,
                FileName = viewModel.FileName,
                Parameters = new List<AnalysisParameter>
                {
                    new AnalysisParameter(viewModel.FirstParameter, viewModel.FirstParameterValue),
                    new AnalysisParameter(viewModel.SecondParameter, viewModel.SecondParameterValue)
                }
            };
        }

        public static TwoSampleAnalyzerInput Create(TwoSampleViewModel viewModel, DataFrame dataFrameA, DataFrame dataFrameB)
        {
            return new TwoSampleAnalyzerInput()
            {
                DataFrameA = dataFrameA,
                FileNameA = viewModel.FileNameA,
                DataFrameB = dataFrameB,
                FileNameB = viewModel.FileNameB,
                Parameters = new List<AnalysisParameter>()
                {
                    new AnalysisParameter(viewModel.FirstParameter, viewModel.FirstParameterValue),
                    new AnalysisParameter(viewModel.SecondParameter, viewModel.SecondParameterValue)
                }
            };
        }

        public static RanovaAnalyzerInput CreateRanova(List<FileDataFrame> fileDataFrames)
        {
            List<DoubleVector> data = new List<DoubleVector>();

            for (int i = 0; i < fileDataFrames.Count; i++)
            {
                data.Add(new DoubleVector(fileDataFrames[i].DataFrame["Values"].ToDoubleVector()));
            }

            List<DFNumericColumn> columns = new List<DFNumericColumn>();

            for (int i = 0; i < data.Count; i++)
            {
                columns.Add(new DFNumericColumn(fileDataFrames[i].FileName, data[i]));
            }

            DataFrame df = new DataFrame(columns.ToArray());

            return new RanovaAnalyzerInput
            {
                Data = df,
                FileNames = fileDataFrames.Select(x => x.FileName).ToList()
            };
        }

        public static AnovaAnalyzerInput Create(List<FileDataFrame> fileDataFrames)
        {
            List<DoubleVector> data = new List<DoubleVector>();

            for (int i = 0; i < fileDataFrames.Count; i++)
            {
                data.Add(new DoubleVector(fileDataFrames[i].DataFrame["Values"].ToDoubleVector()));
            }

            return new AnovaAnalyzerInput
            {
                Data = data.ToArray(),
                FileNames = fileDataFrames.Select(x => x.FileName).ToList()
            };
        }
    }
}
