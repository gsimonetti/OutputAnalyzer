using System.ComponentModel;

namespace OutputAnalyzer.ViewModels
{
    public enum SelectedFileStatus
    {
        [Description("Invalid")]
        Invalid,
        [Description("OK")]
        Ok,
        [Description("Pending")]
        Pending,
        [Description("Processing")]
        Processing
    }
}
