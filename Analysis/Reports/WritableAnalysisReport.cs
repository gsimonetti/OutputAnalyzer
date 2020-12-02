using System.Collections.Generic;

namespace OutputAnalyzer.Analysis.Reports
{
    public class WritableAnalysisReport
    {
        public List<string> Header { get; set; }
        public List<NumericResult> Results { get; set; }
        public List<string> Footer { get; set; }
    }
}
