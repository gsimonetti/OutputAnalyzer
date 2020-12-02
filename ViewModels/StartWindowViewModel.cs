using Avalonia.Controls.ApplicationLifetimes;
using OutputAnalyzer.Analysis.Analyzers;
using OutputAnalyzer.Analysis.Factories;
using OutputAnalyzer.Analysis.Inputs;
using OutputAnalyzer.Analysis.Reports;
using OutputAnalyzer.Models;
using OutputAnalyzer.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OutputAnalyzer.ViewModels
{
    public class StartWindowViewModel : ViewModelBase
    {
        public bool IsAbleToRunAnalysis { get => isAbleToRunAnalysis; set => this.RaiseAndSetIfChanged(ref isAbleToRunAnalysis, value); }
        public bool HasExactlyTwoSamples { get => hasExactlyTwoSamples; set => this.RaiseAndSetIfChanged(ref hasExactlyTwoSamples, value); }
        public bool HasMoreThanOneSample { get => hasMoreThanOneSample; set => this.RaiseAndSetIfChanged(ref hasMoreThanOneSample, value); }
        public string ResultText { get => resultText; set => this.RaiseAndSetIfChanged(ref resultText, value); }
        public double LoadDataFramesProgressBarValue { get => loadDataFramesProgressBarValue; set => this.RaiseAndSetIfChanged(ref loadDataFramesProgressBarValue, value); }
        public double AnalysisProgressBarValue { get => analysisProgressBarValue; set => this.RaiseAndSetIfChanged(ref analysisProgressBarValue, value); }
        public ObservableCollection<SelectedFileViewModel> SelectedFiles { get; set; }
        public ObservableCollection<OneParameterSampleViewModel> AndersonDarlingInputs { get; set; }
        public ObservableCollection<OneParameterSampleViewModel> OneSampleTTestInputs { get; set; }
        public ObservableCollection<TwoParameterSampleViewModel> OneSampleZTestInputs { get; set; }
        public TwoSampleViewModel TwoSampleFTestInputs { get; set; }
        public TwoSampleViewModel TwoSamplePairedTTestInputs { get; set; }
        public TwoSampleViewModel TwoSampleUnpairedTTestInputs { get; set; }
        public TwoSampleViewModel TwoSampleUnpairedUnequalTTestInputs { get; set; }
        public ObservableCollection<SampleNameViewModel> OneWayAnovaInputs { get; set; }
        public ObservableCollection<SampleNameViewModel> OneWayRanovaInputs { get; set; }

        private List<SelectedFileViewModel> validSelectedFiles;
        private bool isAbleToRunAnalysis;
        private bool hasMoreThanOneSample;
        private bool hasExactlyTwoSamples;
        private string resultText;
        private double loadDataFramesProgressBarValue;
        private double analysisProgressBarValue;

        // TODO: improve layout.
        // TODO: use all parameters.
        public StartWindowViewModel()
        {
            SelectedFiles = new ObservableCollection<SelectedFileViewModel>();
            validSelectedFiles = new List<SelectedFileViewModel>();

            AndersonDarlingInputs = new ObservableCollection<OneParameterSampleViewModel>();
            OneSampleTTestInputs = new ObservableCollection<OneParameterSampleViewModel>();
            OneSampleZTestInputs = new ObservableCollection<TwoParameterSampleViewModel>();

            TwoSampleFTestInputs = new TwoSampleViewModel(AnalysisParameterType.Alpha, AnalysisParameterType.HypothesisType);
            TwoSamplePairedTTestInputs = new TwoSampleViewModel(AnalysisParameterType.Alpha, AnalysisParameterType.HypothesisType);
            TwoSampleUnpairedTTestInputs = new TwoSampleViewModel(AnalysisParameterType.Alpha, AnalysisParameterType.HypothesisType);
            TwoSampleUnpairedUnequalTTestInputs = new TwoSampleViewModel(AnalysisParameterType.Alpha, AnalysisParameterType.HypothesisType);

            OneWayAnovaInputs = new ObservableCollection<SampleNameViewModel>();
            OneWayRanovaInputs = new ObservableCollection<SampleNameViewModel>();
        }

        public async Task OnSelectFileCommand()
        {
            if (App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                List<string> filePaths = await FileSelectionService.OpenDataFileSelectionAsync(desktop.MainWindow);

                if (!(filePaths?.Count > 0))
                {
                    return;
                }

                foreach (string path in filePaths)
                {
                    SelectedFiles.Add(new SelectedFileViewModel(path));
                }

                foreach (SelectedFileViewModel selectedFile in SelectedFiles.Where(x => x.Status == SelectedFileStatus.Pending))
                {
                    selectedFile.Status = SelectedFileStatus.Processing;

                    if (await FileSelectionService.IsDataFileValid(selectedFile.FilePath))
                    {
                        selectedFile.Status = SelectedFileStatus.Ok;
                        validSelectedFiles.Add(selectedFile);
                    }
                    else
                    {
                        selectedFile.Status = SelectedFileStatus.Invalid;
                    }
                }

                // One test per sample.
                foreach (SelectedFileViewModel file in validSelectedFiles)
                {
                    AndersonDarlingInputs.Add(new OneParameterSampleViewModel(file.Name, AnalysisParameterType.Alpha));
                    OneSampleTTestInputs.Add(new OneParameterSampleViewModel(file.Name, AnalysisParameterType.PopulationMean));
                    OneSampleZTestInputs.Add(new TwoParameterSampleViewModel(file.Name, AnalysisParameterType.PopulationMean, AnalysisParameterType.PopulationStandardDeviation));
                }

                // One test per two samples.
                if (HasExactlyTwoSamples = SelectedFiles.Count == 2)
                {
                    TwoSampleFTestInputs.FileNameA = SelectedFiles[0].Name;
                    TwoSampleFTestInputs.FileNameB = SelectedFiles[1].Name;
                    TwoSampleFTestInputs.IsEnabled = true;

                    TwoSamplePairedTTestInputs.FileNameA = SelectedFiles[0].Name;
                    TwoSamplePairedTTestInputs.FileNameB = SelectedFiles[1].Name;
                    TwoSamplePairedTTestInputs.IsEnabled = true;

                    TwoSampleUnpairedTTestInputs.FileNameA = SelectedFiles[0].Name;
                    TwoSampleUnpairedTTestInputs.FileNameB = SelectedFiles[1].Name;
                    TwoSampleUnpairedTTestInputs.IsEnabled = true;

                    TwoSampleUnpairedUnequalTTestInputs.FileNameA = SelectedFiles[0].Name;
                    TwoSampleUnpairedUnequalTTestInputs.FileNameB = SelectedFiles[1].Name;
                    TwoSampleUnpairedUnequalTTestInputs.IsEnabled = true;
                }

                // One test per N samples.
                if (HasMoreThanOneSample = SelectedFiles.Count > 1)
                {
                    foreach (SelectedFileViewModel file in validSelectedFiles)
                    {
                        OneWayAnovaInputs.Add(new SampleNameViewModel(file.Name));
                        OneWayRanovaInputs.Add(new SampleNameViewModel(file.Name));
                    }
                }

                validSelectedFiles = SelectedFiles.Where(x => x.Status == SelectedFileStatus.Ok).ToList();

                IsAbleToRunAnalysis = validSelectedFiles.Any();
            }
        }

        public void OnClearFilesCommand()
        {
            ResetInputs();
            ResetProgress();
        }

        public async Task OnRunAndSaveCommand()
        {
            ResetProgress();

            string reportFileDestinationPath = await GetReportFileDestinationFromDialogAsync();

            if (string.IsNullOrEmpty(reportFileDestinationPath))
            {
                return;
            }

            List<FileDataFrame> fileDataFrames = await CreateFileDataFramesFromFiles();

            // Execute analysis.
            List<WritableAnalysisReport> reports = new List<WritableAnalysisReport>();

            List<AnalyzerFactory> factories = new List<AnalyzerFactory>();

            factories.AddRange(FactoryCreatorService.CreateAndersonDarlingFactories(AndersonDarlingInputs.ToList(), fileDataFrames));
            factories.AddRange(FactoryCreatorService.CreateOneSampleTTestFactories(OneSampleTTestInputs.ToList(), fileDataFrames));
            factories.AddRange(FactoryCreatorService.CreateOneSampleZTestFactories(OneSampleZTestInputs.ToList(), fileDataFrames));

            if (HasExactlyTwoSamples)
            {
                factories.Add(new TwoSampleFTestFactory(AnalyzerInputCreatorService.Create(TwoSampleFTestInputs, fileDataFrames[0].DataFrame, fileDataFrames[1].DataFrame)));
                factories.Add(new TwoSamplePairedTTestFactory(AnalyzerInputCreatorService.Create(TwoSamplePairedTTestInputs, fileDataFrames[0].DataFrame, fileDataFrames[1].DataFrame)));
                factories.Add(new TwoSampleUnpairedTTestFactory(AnalyzerInputCreatorService.Create(TwoSampleUnpairedTTestInputs, fileDataFrames[0].DataFrame, fileDataFrames[1].DataFrame)));
                factories.Add(new TwoSampleUnpairedUnequalTTestFactory(AnalyzerInputCreatorService.Create(TwoSampleUnpairedUnequalTTestInputs, fileDataFrames[0].DataFrame, fileDataFrames[1].DataFrame)));
            }

            if (HasMoreThanOneSample)
            {
                factories.Add(new OneWayAnovaFactory(AnalyzerInputCreatorService.Create(fileDataFrames)));
                factories.Add(new OneWayRanovaFactory(AnalyzerInputCreatorService.CreateRanova(fileDataFrames)));
            }

            foreach (AnalyzerFactory factory in factories)
            {
                IReportableAnalyzer analyzer = factory.Create();
                reports.Add(analyzer.GetAnalysisReport());
            }

            AnalysisProgressBarValue += 100.0d;

            // Save file.
            await ReportFileService.CreateReportsFileAsync(reportFileDestinationPath, reports);

            // Print result.
            ResultText = "Result: OK";

            // Clear memory.
            foreach (FileDataFrame fileDataFrame in fileDataFrames)
            {
                fileDataFrame.DataFrame.Clear();
            }
        }

        private static async Task<string> GetReportFileDestinationFromDialogAsync()
        {
            string response = string.Empty;

            if (App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                string folderPath = await FileSelectionService.OpenFolderSelectionAsync(desktop.MainWindow);
                string fileName = $"results_{DateTime.Now.Ticks}.txt";

                response = Path.Combine(folderPath, fileName);
            }

            return response;
        }

        private async Task<List<FileDataFrame>> CreateFileDataFramesFromFiles()
        {
            List<FileDataFrame> fileDataFrames = new List<FileDataFrame>();

            double delta = Math.Ceiling(100.0d / SelectedFiles.Where(x => x.Status == SelectedFileStatus.Ok).Count());

            foreach (SelectedFileViewModel selectedFile in SelectedFiles.Where(x => x.Status == SelectedFileStatus.Ok))
            {
                fileDataFrames.Add(new FileDataFrame(selectedFile.Name, await FileReaderService.LoadDataFrameFromFile(selectedFile.FilePath)));
                LoadDataFramesProgressBarValue += delta;
            }

            return fileDataFrames;
        }

        private void ResetProgress()
        {
            LoadDataFramesProgressBarValue = 0.0d;
            AnalysisProgressBarValue = 0.0d;

            ResultText = "Result: -";
        }

        private void ResetInputs()
        {
            IsAbleToRunAnalysis = false;
            HasMoreThanOneSample = false;
            HasExactlyTwoSamples = false;

            SelectedFiles.Clear();
            validSelectedFiles.Clear();

            AndersonDarlingInputs.Clear();
            OneSampleTTestInputs.Clear();
            OneSampleZTestInputs.Clear();

            TwoSampleFTestInputs.Reset();
            TwoSamplePairedTTestInputs.Reset();
            TwoSampleUnpairedTTestInputs.Reset();
            TwoSampleUnpairedUnequalTTestInputs.Reset();

            OneWayAnovaInputs.Clear();
            OneWayRanovaInputs.Clear();
        }
    }
}
