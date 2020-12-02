using OutputAnalyzer.Analysis.Inputs;
using OutputAnalyzer.Extensions;
using ReactiveUI;

namespace OutputAnalyzer.ViewModels
{
    public class OneParameterSampleViewModel : ViewModelBase
    {
        public string FileName { get => fileName; set => this.RaiseAndSetIfChanged(ref fileName, value); }
        public string ParameterName { get => parameterName; set => this.RaiseAndSetIfChanged(ref parameterName, value); }
        public string ParameterValue { get => parameterValue; set => this.RaiseAndSetIfChanged(ref parameterValue, value); }

        public AnalysisParameterType FirstParameter { get; private set; }

        private string fileName;
        private string parameterName;
        private string parameterValue;

        public OneParameterSampleViewModel(string fileName, AnalysisParameterType parameter)
        {
            FileName = fileName;
            FirstParameter = parameter;
            ParameterName = FirstParameter.GetDescription();
            ParameterValue = GetDefaultParameterValue(parameter);
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
