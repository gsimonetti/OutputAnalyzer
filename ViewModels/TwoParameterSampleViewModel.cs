using OutputAnalyzer.Analysis.Inputs;
using OutputAnalyzer.Extensions;
using ReactiveUI;

namespace OutputAnalyzer.ViewModels
{
    public class TwoParameterSampleViewModel : ViewModelBase
    {
        public string FileName { get => fileName; set => this.RaiseAndSetIfChanged(ref fileName, value); }
        public string FirstParameterName { get => firstParameterName; set => this.RaiseAndSetIfChanged(ref firstParameterName, value); }
        public string FirstParameterValue { get => firstParameterValue; set => this.RaiseAndSetIfChanged(ref firstParameterValue, value); }
        public string SecondParameterName { get => secondParameterName; set => this.RaiseAndSetIfChanged(ref secondParameterName, value); }
        public string SecondParameterValue { get => secondParameterValue; set => this.RaiseAndSetIfChanged(ref secondParameterValue, value); }

        public AnalysisParameterType FirstParameter { get; private set; }
        public AnalysisParameterType SecondParameter { get; private set; }

        private string fileName;
        private string firstParameterName;
        private string firstParameterValue;
        private string secondParameterName;
        private string secondParameterValue;

        public TwoParameterSampleViewModel(string fileName, AnalysisParameterType firstParameter, AnalysisParameterType secondParameter)
        {
            FileName = fileName;
            FirstParameter = firstParameter;
            SecondParameter = secondParameter;

            FirstParameterName = FirstParameter.GetDescription();
            FirstParameterValue = GetDefaultParameterValue(firstParameter);

            SecondParameterName = SecondParameter.GetDescription();
            SecondParameterValue = GetDefaultParameterValue(secondParameter);
        }

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
