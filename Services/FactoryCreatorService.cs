using CenterSpace.NMath.Core;
using OutputAnalyzer.Analysis.Factories;
using OutputAnalyzer.Models;
using OutputAnalyzer.ViewModels;
using System.Collections.Generic;

namespace OutputAnalyzer.Services
{
    public static class FactoryCreatorService
    {
        public static List<AndersonDarlingAnalyzerFactory> CreateAndersonDarlingFactories(List<OneParameterSampleViewModel> viewModelsList, List<FileDataFrame> fileDataFrames)
        {
            List<AndersonDarlingAnalyzerFactory> response = new List<AndersonDarlingAnalyzerFactory>();

            foreach (OneParameterSampleViewModel sampleViewModel in viewModelsList)
            {
                if (fileDataFrames.Find(x => x.FileName == sampleViewModel.FileName)?.DataFrame is DataFrame dataFrame)
                {
                    response.Add(new AndersonDarlingAnalyzerFactory(AnalyzerInputCreatorService.Create(sampleViewModel, dataFrame)));
                }
            }

            return response;
        }

        public static List<OneSampleTTestAnalyzerFactory> CreateOneSampleTTestFactories(List<OneParameterSampleViewModel> viewModelsList, List<FileDataFrame> fileDataFrames)
        {
            List<OneSampleTTestAnalyzerFactory> response = new List<OneSampleTTestAnalyzerFactory>();

            foreach (OneParameterSampleViewModel sampleViewModel in viewModelsList)
            {
                if (fileDataFrames.Find(x => x.FileName == sampleViewModel.FileName)?.DataFrame is DataFrame dataFrame)
                {
                    response.Add(new OneSampleTTestAnalyzerFactory(AnalyzerInputCreatorService.Create(sampleViewModel, dataFrame)));
                }
            }

            return response;
        }

        public static List<OneSampleZTestFactory> CreateOneSampleZTestFactories(List<TwoParameterSampleViewModel> viewModelsList, List<FileDataFrame> fileDataFrames)
        {
            List<OneSampleZTestFactory> response = new List<OneSampleZTestFactory>();

            foreach (TwoParameterSampleViewModel sampleViewModel in viewModelsList)
            {
                if (fileDataFrames.Find(x => x.FileName == sampleViewModel.FileName)?.DataFrame is DataFrame dataFrame)
                {
                    response.Add(new OneSampleZTestFactory(AnalyzerInputCreatorService.Create(sampleViewModel, dataFrame)));
                }
            }

            return response;
        }
    }
}
