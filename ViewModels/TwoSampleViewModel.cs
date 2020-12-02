using OutputAnalyzer.Analysis.Inputs;
using OutputAnalyzer.Extensions;
using ReactiveUI;

namespace OutputAnalyzer.ViewModels
{
    public class TwoSampleViewModel : ViewModelBase
    {
        public string FileNameA { get => fileNameA; set => this.RaiseAndSetIfChanged(ref fileNameA, value); }
        public string FileNameB { get => fileNameB; set => this.RaiseAndSetIfChanged(ref fileNameB, value); }
        public string FirstParameterName { get => firstParameterName; set => this.RaiseAndSetIfChanged(ref firstParameterName, value); }
        public string FirstParameterValue { get => firstParameterValue; set => this.RaiseAndSetIfChanged(ref firstParameterValue, value); }
        public string SecondParameterName { get => secondParameterName; set => this.RaiseAndSetIfChanged(ref secondParameterName, value); }
        public string SecondParameterValue { get => secondParameterValue; set => this.RaiseAndSetIfChanged(ref secondParameterValue, value); }
        public bool IsEnabled { get => isEnabled; set => this.RaiseAndSetIfChanged(ref isEnabled, value); }

        public AnalysisParameterType FirstParameter { get; private set; }
        public AnalysisParameterType SecondParameter { get; private set; }

        private string fileNameA;
        private string fileNameB;
        private string firstParameterName;
        private string firstParameterValue;
        private string secondParameterName;
        private string secondParameterValue;
        private bool isEnabled;

        public TwoSampleViewModel(AnalysisParameterType firstParameter, AnalysisParameterType secondParameter)
        {
            FileNameA = fileNameA;
            FileNameB = fileNameB;

            FirstParameter = firstParameter;
            SecondParameter = secondParameter;

            FirstParameterName = FirstParameter.GetDescription();
            FirstParameterValue = GetDefaultParameterValue(firstParameter);

            SecondParameterName = SecondParameter.GetDescription();
            SecondParameterValue = GetDefaultParameterValue(secondParameter);
        }

        public void Reset()
        {
            FileNameA = string.Empty;
            FileNameB = string.Empty;
            FirstParameterValue = GetDefaultParameterValue(FirstParameter);
            SecondParameterValue = GetDefaultParameterValue(SecondParameter);
            IsEnabled = false;
        }

        // TODO: remove duplicated methods.
        private static string GetDefaultParameterValue(AnalysisParameterType analysisParameter)
        {
            return analysisParameter switch
            {
                AnalysisParameterType.Alpha => "0.05",
                AnalysisParameterType.HypothesisType => "TwoSided",
                _ => string.Empty,
            };
        }
    }
}
