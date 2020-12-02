using OutputAnalyzer.Extensions;
using ReactiveUI;
using System;
using System.IO;

namespace OutputAnalyzer.ViewModels
{
    public class SelectedFileViewModel : ViewModelBase
    {
        private const int DefaultNumberOfDots = 32;

        public string PresentationText { get => presentationText; set => this.RaiseAndSetIfChanged(ref presentationText, value); }
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    UpdatePresentationText();
                }
            }
        }
        public SelectedFileStatus Status
        {
            get => status;
            set
            {
                if (status != value)
                {
                    status = value;
                    statusName = status.GetDescription();
                    UpdatePresentationText();
                }
            }
        }
        public string FilePath { get; private set; }

        private string presentationText;
        private string name;
        private string statusName;
        private SelectedFileStatus status;

        public SelectedFileViewModel(string filePath)
        {
            FilePath = filePath;
            name = Path.GetFileName(filePath);
            status = SelectedFileStatus.Pending;
            statusName = status.GetDescription();
            UpdatePresentationText();
        }

        private void UpdatePresentationText()
        {
            int sumOfStringSizes = 0;

            if (!string.IsNullOrEmpty(name))
            {
                sumOfStringSizes += name.Length;
            }

            if (!string.IsNullOrEmpty(statusName))
            {
                sumOfStringSizes += statusName.Length;
            }

            int numberOfDots = Math.Clamp(DefaultNumberOfDots - sumOfStringSizes, 0, int.MaxValue);
            PresentationText = $"{Name}{new string('.', numberOfDots)}{statusName}";
        }
    }
}
