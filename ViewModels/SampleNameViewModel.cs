using ReactiveUI;

namespace OutputAnalyzer.ViewModels
{
    public class SampleNameViewModel : ViewModelBase
    {
        public string FileName { get => fileName; set => this.RaiseAndSetIfChanged(ref fileName, value); }

        private string fileName;

        public SampleNameViewModel(string fileName)
        {
            FileName = fileName;
        }
    }
}
